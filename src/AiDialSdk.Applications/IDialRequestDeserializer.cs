using AiDialSdk.Api.Data;
using Microsoft.AspNetCore.Http;

namespace AiDialSdk.Applications;

public interface IDialRequestDeserializer
{
    public Task<DialChatCompletionRequest> DeserializeAsync(HttpRequest request, CancellationToken token);
}