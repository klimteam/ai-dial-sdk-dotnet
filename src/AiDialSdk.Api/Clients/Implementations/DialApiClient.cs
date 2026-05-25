using AiDialSdk.Api.Chat;
using AiDialSdk.Api.Chat.Implementations;
using AiDialSdk.Api.Files;
using AiDialSdk.Api.Files.Implementations;
using AiDialSdk.Api.Infrastructure;
using AiDialSdk.Api.Users;
using AiDialSdk.Api.Users.Implementations;
using Microsoft.Extensions.Options;

namespace AiDialSdk.Api.Clients.Implementations;

public class DialApiClient : IDialApiClient
{
    private readonly HttpClient _httpClient;
    private readonly Uri _endpoint;
    private readonly string? _apiKey;
    
    public DialApiClient(HttpClient httpClient, IOptions<DialClientConfiguration> configuration)
    {
        _httpClient = httpClient;
        _endpoint = configuration.Value.GetBaseUrlOrThrow();
        _apiKey = configuration.Value.ApiKey;
    }
    
    public IDialChatApiClient GetChatClient(string deploymentName, string? apiVersion = null)
    {
        return new DialChatApiClient(_httpClient, _endpoint, _apiKey, deploymentName, apiVersion);
    }

    public IDialUserApiClient GetUserApiClient()
    {
        return new DialUserApiClient(_httpClient, _endpoint, _apiKey);
    }

    public IDialFileApiClient GetFileApiClient()
    {
        return new DialFileApiClient(_httpClient, _endpoint, _apiKey);
    }
}