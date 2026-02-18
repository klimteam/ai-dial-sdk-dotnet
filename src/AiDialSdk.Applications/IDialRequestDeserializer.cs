using AiDialSdk.Api.Data;
using Microsoft.AspNetCore.Http;

public interface IDialRequestDeserializer
{
    public Task<DialChatCompletionRequest> DeserializeAsync(HttpRequest request, CancellationToken token);
}