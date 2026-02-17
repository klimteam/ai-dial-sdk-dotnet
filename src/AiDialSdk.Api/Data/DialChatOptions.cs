using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialChatOptions
{
    public int? MaxCompletionTokens { get; set; }
    
    public ResponseFormat? ResponseFormat { get; set; }
    
    public int? Seed { get; set; }
    
    internal bool? Stream { get; set; }
    
    public float? Temperature { get; set; }
    
    public float? TopP { get; set; }
    
    public ToolChoiceMode? ToolChoice { get; set; }
    
    public List<Tool>? Tools { get; set; }
    
    public DialRequestCustomFields? CustomFields { get; set; }
}