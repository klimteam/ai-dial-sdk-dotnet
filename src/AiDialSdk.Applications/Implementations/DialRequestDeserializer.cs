using AiDialSdk.Api.Data;
using AiDialSdk.Api.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Implementations;

public class DialRequestDeserializer : IDialRequestDeserializer
{
    public async Task<DialChatCompletionRequest> DeserializeAsync(HttpRequest request, CancellationToken token)
    {
        var chatCompletionRequest = await request.ReadFromJsonAsync<DialChatCompletionRequest>(
            GlobalJsonSettings.ChatCompletionRequestJsonSerializerOptions, 
            token) ?? throw new NullReferenceException("Failed to deserialize DialChatCompletionRequest");

        return chatCompletionRequest;
    }
}