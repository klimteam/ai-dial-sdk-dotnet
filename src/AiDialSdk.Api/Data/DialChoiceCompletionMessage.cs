using System.Text.Json.Serialization;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialChoiceCompletionMessage : ChoiceCompletionMessage
{
    [JsonConstructor]
    public DialChoiceCompletionMessage(Role? role, string? content, string? refusal, IReadOnlyList<ToolCall>? toolCalls, 
        DialCustomContent? customContent) : base(role, content, refusal, toolCalls)
    {
        CustomContent = customContent;
    }
    
    public DialChoiceCompletionMessage(DialCustomContent content) 
        : base(null, null, null, null)
    {
        CustomContent = content;
    }
    
    public DialCustomContent? CustomContent { get; }
    
    public override DialChoiceCompletionMessage Combine(ChoiceCompletionMessage choiceCompletionMessage)
    {
        var customContent = CustomContent;
        
        if (choiceCompletionMessage is DialChoiceCompletionMessage { CustomContent: not null } dialChoiceCompletionMessage)
        {
            customContent = customContent == null
                ? dialChoiceCompletionMessage.CustomContent
                : customContent.Combine(dialChoiceCompletionMessage.CustomContent);
        }
        
        var tempCombinedChoiceCompletionMessage = base.Combine(choiceCompletionMessage);
        
        return new DialChoiceCompletionMessage(
            tempCombinedChoiceCompletionMessage.Role,
            tempCombinedChoiceCompletionMessage.Content,
            tempCombinedChoiceCompletionMessage.Refusal,
            tempCombinedChoiceCompletionMessage.ToolCalls,
            customContent);
    }
}