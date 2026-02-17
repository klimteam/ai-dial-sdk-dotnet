using System.Text.Json.Serialization;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialUserMessage : UserMessage
{
    [JsonConstructor]
    public DialUserMessage(string content, string? name = null, UserMessageCustomContent? customContent = null) 
        : base(content, name)
    {
        CustomContent = customContent;
    }
    
    public UserMessageCustomContent? CustomContent { get; }
    
    [JsonIgnore]
    public bool HasCustomContent => CustomContent is not null;
}