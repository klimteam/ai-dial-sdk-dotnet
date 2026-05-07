using System.Text.Json;
using AiDialSdk.Api.Clients.Implementations;
using AiDialSdk.Api.Data;
using AiDialSdk.Api.Infrastructure;

namespace AiDialSdk.Api.Conversations.Implementations;

public class DialConversationApiClient : BaseApiClient, IDialConversationApiClient
{
    public DialConversationApiClient(HttpClient httpClient, Uri endpoint, string? apiKey) 
        : base(httpClient, endpoint, apiKey)
    {
    }

    public async Task<DialConversation> GetConversationAsync(string conversationId, CancellationToken token)
    {
        var content = await GetConversationJsonInternalAsync(conversationId, token);
        return JsonSerializer.Deserialize<DialConversation>(
                   content,
                   GlobalJsonSettings.ChatCompletionResponseJsonSerializerOptions)
               ?? throw new Exception("Failed to deserialize the conversation response.");
    }

    public async Task<string> GetConversationAsJsonAsync(string conversationId, CancellationToken token)
    {
        var content = await GetConversationJsonInternalAsync(conversationId, token);
        return content;
    }
    
    private async Task<string> GetConversationJsonInternalAsync(string conversationId, CancellationToken token)
    {
        var conversationUrl = new Uri(Endpoint, $"/v1/{conversationId}");
        var request = new HttpRequestMessage(HttpMethod.Get, conversationUrl);
        var response = await SendAsync(request, token);
        
        return await response.Content.ReadAsStringAsync(token);
    }
}