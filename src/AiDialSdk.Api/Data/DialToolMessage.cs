using System.Text.Json.Serialization;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialToolMessage(string content, string toolCallId, DialCustomContent? customContent = null) 
    : ToolMessage(content, toolCallId)
{
    public DialCustomContent? CustomContent { get; } = customContent;
    
    [JsonIgnore]
    public bool HasCustomContent => CustomContent is not null;
}