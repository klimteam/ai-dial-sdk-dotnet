using AiDialSdk.Api.Data;
using AiDialSdk.Api.OpenAi.Data;

namespace EchoDialApp;

public class EchoDialApplication : IDialApplication
{
    public async Task CompleteChoicesAsync(DialChoiceCompletionContext completionContext, CancellationToken token)
    {
        var choice = completionContext.Choices.Single();

        if (completionContext.ChatCompletionRequest.Messages.Last() is UserMessage message)
        {
            await choice.AppendContentAsync(message.Content, token);
        }
    }

    public Task<DialFormSchema> GetConfigurationAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}