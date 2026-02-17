using System.Text.Json;
using AiDialSdk.Api.Clients.Implementations;
using AiDialSdk.Api.Data;
using AiDialSdk.Api.Infrastructure;

namespace AiDialSdk.Api.Users.Implementations;

public class DialUserApiClient : BaseApiClient, IDialUserApiClient
{
    public DialUserApiClient(HttpClient httpClient, Uri endpoint, string? apiKey) : base(httpClient, endpoint, apiKey)
    {
    }

    public async Task<UserInfo> GetUserInfoAsync(CancellationToken token)
    {
        var userInfoUrl = BuildUserInfoUri(Endpoint);
        
        var request = new HttpRequestMessage(HttpMethod.Get, userInfoUrl);

        var response = await SendAsync(request, token);
        
        var content = await response.Content.ReadAsStringAsync(token);
        
        return JsonSerializer.Deserialize<UserInfo>(content, GlobalJsonSettings.DefaultJsonSerializerOptions) 
               ?? throw new Exception("User info is null");
    }
    
    private static Uri BuildUserInfoUri(Uri endpoint) => new($"{endpoint}/v1/user/info");
}