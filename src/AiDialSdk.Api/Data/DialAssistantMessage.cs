using System.Text.Json.Serialization;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialAssistantMessage : AssistantMessage
{
    [JsonConstructor]
    public DialAssistantMessage(
        string? content = null, 
        string? name = null, 
        string? refusal = null, 
        IReadOnlyList<ToolCall>? toolCalls = null, 
        DialCustomContent? customContent = null) : base(content, name, refusal, toolCalls)
    {
        CustomContent = customContent;
    }
    
    public DialCustomContent? CustomContent { get; }
}