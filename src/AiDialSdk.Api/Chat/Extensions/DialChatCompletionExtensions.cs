using AiDialSdk.Api.Data;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Chat.Extensions;

public static class DialChatCompletionExtensions
{
    public static DialAssistantMessage ToDialAssistantMessage(this DialChoiceCompletionMessage dialChoiceCompletionMessage)
    {
        if (dialChoiceCompletionMessage.Role != Role.Assistant)
            throw new NotSupportedException("Choice completion message role must be assistant");
        
        return new DialAssistantMessage(
            dialChoiceCompletionMessage.Content,
            null,
            dialChoiceCompletionMessage.Refusal, 
            dialChoiceCompletionMessage.ToolCalls,
            dialChoiceCompletionMessage.CustomContent);
    }
}