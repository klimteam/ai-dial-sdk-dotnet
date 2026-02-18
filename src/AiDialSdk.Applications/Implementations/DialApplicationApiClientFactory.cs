using AiDialSdk.Api.Clients;
using AiDialSdk.Api.Clients.Implementations;
using Infrastructures;
using Microsoft.Extensions.Options;

namespace Implementations;

using DialApiClient = DialApiClient;

public class DialApplicationApiClientFactory<TDialContext> : IDialApiClientFactory where TDialContext : BaseContext
{
    private const string DialApiClientName = "DialApiClient";
    
    private readonly IDialContextAccessor<TDialContext> _contextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly DialApplicationConfiguration _configuration;
    
    public DialApplicationApiClientFactory(
        IDialContextAccessor<TDialContext> contextAccessor, 
        IHttpClientFactory httpClientFactory,
        IOptions<DialApplicationConfiguration> configuration)
    {
        _contextAccessor = contextAccessor;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration.Value;
    }
    
    public IDialApiClient CreateDialApiClient()
    {
        return new DialApiClient(
            _httpClientFactory.CreateClient(DialApiClientName),
            _configuration.GetUriOrThrow(),
            _contextAccessor.Context.ApiKey);
    }
}