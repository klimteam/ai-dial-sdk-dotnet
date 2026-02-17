using System.Text;
using System.Text.Json.Serialization;

namespace AiDialSdk.Api.OpenAi.Data;

public class ChoiceCompletionMessage(Role? role, string? content, string? refusal, IReadOnlyList<ToolCall>? toolCalls)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Role? Role { get; } = role;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Content { get; } = content;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Refusal { get; } = refusal;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<ToolCall>? ToolCalls { get; } = toolCalls;

    public virtual ChoiceCompletionMessage Combine(ChoiceCompletionMessage other)
    {
        var toolCalls = ToolCalls?.ToList();
        if (toolCalls is null)
        {
            toolCalls = other.ToolCalls?.ToList();
        }
        else if (other.ToolCalls is not null)
        {
            foreach (var newToolCall in other.ToolCalls)
            {
                if (newToolCall.Index != toolCalls.Count)
                    throw new Exception("Tool call indexes must be sequential");
                toolCalls.Add(newToolCall);
            }
        }

        StringBuilder? content = null;
        if (Content is not null)
        {
            content = new StringBuilder(Content);
        }

        if (other.Content is null)
            return new ChoiceCompletionMessage(other.Role ?? Role, content?.ToString(), other.Refusal ?? Refusal,
                toolCalls);
        
        if (content is null)
        {
            content = new StringBuilder(other.Content);
        }
        else
        {
            content.Append(other.Content);
        }

        return new ChoiceCompletionMessage(
            other.Role ?? Role,
            content.ToString(), 
            other.Refusal ?? Refusal,
            toolCalls);
    }
}