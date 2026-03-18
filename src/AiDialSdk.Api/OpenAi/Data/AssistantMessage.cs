using System.Text.Json.Serialization;

namespace AiDialSdk.Api.OpenAi.Data;

public class AssistantMessage : BaseMessage
{
    [JsonConstructor]
    public AssistantMessage(string? content = null, string? name = null, string? refusal = null, IReadOnlyList<ToolCall>? toolCalls = null) : base(Role.Assistant)
    {
        Content = content;
        Name = name;
        Refusal = refusal;
        ToolCalls = toolCalls;
    }

    public AssistantMessage(string content) : base(Role.Assistant)
    {
        Content = content;
    }
    
    public string? Content { get; }
    
    public string? Name { get; }
    
    public string? Refusal { get; }

    public IReadOnlyList<ToolCall>? ToolCalls { get; }
}