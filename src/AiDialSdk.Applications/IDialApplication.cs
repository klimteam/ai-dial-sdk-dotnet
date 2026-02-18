using AiDialSdk.Api.Data;

public interface IDialApplication
{
    Task CompleteChoicesAsync(DialChoiceCompletionContext completionContext, CancellationToken token);
    
    Task<DialFormSchema> GetConfigurationAsync(CancellationToken token);
}