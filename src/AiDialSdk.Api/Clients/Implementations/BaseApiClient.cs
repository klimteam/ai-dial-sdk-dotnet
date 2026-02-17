namespace AiDialSdk.Api.Clients.Implementations;

public abstract class BaseApiClient
{
    private readonly string? _apiKey;
    
    protected BaseApiClient(HttpClient httpClient, Uri endpoint, string? apiKey)
    {
        HttpClient = httpClient;
        Endpoint = endpoint;
        _apiKey = apiKey;
    }
    
    protected HttpClient HttpClient { get; }
    
    protected Uri Endpoint { get; }
    
    protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken token)
    {
        ConfigureRequestHeaders(httpRequestMessage);
        
        var response = await HttpClient.SendAsync(httpRequestMessage, token);
        response.EnsureSuccessStatusCode();
        
        return response;
    }
    
    protected void ConfigureRequestHeaders(HttpRequestMessage httpRequestMessage)
    {
        if (!string.IsNullOrEmpty(_apiKey))
        {
            httpRequestMessage.Headers.Add("api-key", _apiKey);
        }
    }
}