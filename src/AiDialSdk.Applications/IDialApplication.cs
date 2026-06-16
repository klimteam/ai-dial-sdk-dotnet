using AiDialSdk.Api.Data;

namespace AiDialSdk.Applications;

public interface IDialApplication
{
    Task CompleteChoicesAsync(DialChoiceCompletionContext completionContext, CancellationToken token);
    
    Task<DialFormSchema> GetConfigurationAsync(CancellationToken token);
}