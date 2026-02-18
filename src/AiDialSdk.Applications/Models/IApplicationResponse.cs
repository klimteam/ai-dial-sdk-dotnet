using AiDialSdk.Api.Data;
using AiDialSdk.Api.OpenAi.Data;

namespace Models;

public interface IApplicationResponse
{
    Task AppendDeltaChoiceAsync(DialDeltaChoice deltaChoice, CancellationToken token);

    Task AppendUsageAsync(Usage usage, CancellationToken token);
    
    Task DoneAsync(CancellationToken token);
}