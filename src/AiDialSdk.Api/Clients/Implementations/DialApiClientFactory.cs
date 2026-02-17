using AiDialSdk.Api.Infrastructure;
using Microsoft.Extensions.Options;

namespace AiDialSdk.Api.Clients.Implementations;

public class DialApiClientFactory : IDialApiClientFactory
{
    private readonly HttpClient _httpClient;
    private readonly DialClientConfiguration _configuration;

    public DialApiClientFactory(HttpClient httpClient, IOptions<DialClientConfiguration> configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration.Value;
    }
    
    public IDialApiClient CreateDialApiClient()
    {
        return new DialApiClient(_httpClient, _configuration.GetUriOrThrow(), _configuration.GetApiKeyOrThrow());
    }
}