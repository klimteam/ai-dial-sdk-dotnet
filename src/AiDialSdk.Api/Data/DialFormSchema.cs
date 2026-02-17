using System.Text.Json.Serialization;
using AiDialSdk.Api.Extensions;

namespace AiDialSdk.Api.Data;

public class DialFormSchema
{
    [JsonConstructor]
    public DialFormSchema(bool chatMessageInputDisabled, IReadOnlyDictionary<string, AiDialBaseControl> properties, IReadOnlyCollection<string> required)
    {
        ChatMessageInputDisabled = chatMessageInputDisabled;
        Properties = properties;
        Required = required;
    }
 
    public DialFormSchema(bool chatMessageInputDisabled, IReadOnlyDictionary<string, AiDialBaseControl> properties) 
        : this(chatMessageInputDisabled, properties, [])
    {
    }
    
    public SimpleType Type => SimpleType.Object;
    
    [JsonPropertyName("dial:chatMessageInputDisabled")]
    public bool ChatMessageInputDisabled { get; }
    
    public bool AdditionalProperties => false;

    public IReadOnlyCollection<string> Required { get; }
    
    public IReadOnlyDictionary<string, AiDialBaseControl> Properties { get; }
    
    public DialFormSchema Combine(DialFormSchema other)
    {
        return new DialFormSchema(
            ChatMessageInputDisabled || other.ChatMessageInputDisabled, 
            Properties.Combine(other.Properties) ?? new Dictionary<string, AiDialBaseControl>(), 
            Required.Combine(other.Required) ?? Array.Empty<string>());
    }
    
    public static DialFormSchema EmptyChatEnabled { get; } = new(false, new Dictionary<string, AiDialBaseControl>(),Array.Empty<string>());
    
    public static DialFormSchema EmptyChatDisabled { get; } = new(true, new Dictionary<string, AiDialBaseControl>(),Array.Empty<string>());
}