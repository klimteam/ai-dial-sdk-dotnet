using AiDialSdk.Api.Clients;
using AiDialSdk.Applications.Infrastructures;
using Microsoft.Extensions.Options;

namespace AiDialSdk.Applications.Implementations;

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
        throw new NotImplementedException();
    }
}