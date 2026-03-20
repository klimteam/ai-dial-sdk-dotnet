using AiDialSdk.Api.Clients.Implementations;

namespace AiDialSdk.Api.Conversations.Implementations;

public class DialConversationApiClient : BaseApiClient, IDialConversationApiClient
{
    public DialConversationApiClient(HttpClient httpClient, Uri endpoint, string? apiKey) 
        : base(httpClient, endpoint, apiKey)
    {
    }

    public async Task GetConversationAsync(string conversationId, CancellationToken token)
    {
        var conversationUrl = new Uri(Endpoint, $"/v1/{conversationId}");
        var request = new HttpRequestMessage(HttpMethod.Get, conversationUrl);
        var response = await SendAsync(request, token);
        
        var content = await response.Content.ReadAsStringAsync(token);
        
        // Handle the response as needed (e.g., deserialize the content)
    }
}