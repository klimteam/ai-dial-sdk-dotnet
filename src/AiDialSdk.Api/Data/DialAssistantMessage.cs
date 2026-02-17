using System.Text.Json.Serialization;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialAssistantMessage : AssistantMessage
{
    [JsonConstructor]
    public DialAssistantMessage(string? content = null, string? refusal = null, IReadOnlyList<ToolCall>? toolCalls = null, DialCustomContent? customContent = null)
        : base(content, refusal, toolCalls)
    {
        CustomContent = customContent;
    }
    
    public DialCustomContent? CustomContent { get; }
}