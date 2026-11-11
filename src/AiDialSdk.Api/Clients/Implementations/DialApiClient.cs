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
    private readonly DialClientConfiguration _configuration;
    
    public DialApiClient(HttpClient httpClient, IOptions<DialClientConfiguration> configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration.Value;
    }
    
    public IDialChatApiClient GetChatClient(string deploymentName, string? apiVersion = null)
    {
        return new DialChatApiClient(_httpClient, _configuration.GetBaseUrlOrThrow(), _configuration.ApiKey, deploymentName, apiVersion);
    }

    public IDialUserApiClient GetUserApiClient()
    {
        return new DialUserApiClient(_httpClient, _configuration.GetBaseUrlOrThrow(), _configuration.ApiKey);
    }

    public IDialFileApiClient GetFileApiClient()
    {
        return new DialFileApiClient(_httpClient, _configuration.GetBaseUrlOrThrow(), _configuration.ApiKey);
    }
}