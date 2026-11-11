using AiDialSdk.Api.Data;
using AiDialSdk.Applications;

namespace EchoDialApp;

public class EchoDialApplication : IDialApplication
{
    public async Task CompleteChoicesAsync(DialChoiceCompletionContext completionContext, CancellationToken token)
    {
        var choice = completionContext.Choices.Single();
        var inputMessage = completionContext.ChatCompletionRequest.Messages.LastOrDefault();
        
        if (inputMessage is not DialUserMessage dialUserMessage)
        {
            return;
        }
        
        await choice.AppendContentAsync(dialUserMessage.Content, token);
    }

    public Task<DialFormSchema> GetConfigurationAsync(CancellationToken token)
    {
        return Task.FromResult(DialFormSchema.EmptyChatEnabled);
    }
}