using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Implementations;

public class ConfigurationInvoker : IConfigurationInvoker
{
    private readonly IDialContextAccessor<BaseContext> _dialContextAccessor;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ConfigurationInvoker> _logger;

    public ConfigurationInvoker(
        IDialContextAccessor<BaseContext> dialContextAccessor,
        IServiceProvider serviceProvider,
        ILogger<ConfigurationInvoker> logger)
    {
        _dialContextAccessor = dialContextAccessor;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public async Task InvokeAsync(string deploymentName, HttpRequest request, HttpResponse response,
        CancellationToken token)
    {
        var completionContext = CreateContext(deploymentName, request, response);
        
        if (_dialContextAccessor is IDialContextSetter<BaseContext> setter)
        {
            setter.SetCompletionContext(completionContext);
        }

        var dialApplication = _serviceProvider.GetRequiredService<IDialApplication>();
            
        var configuration = await dialApplication.GetConfigurationAsync(token);

        var configurationContent = JsonSerializer.Serialize(configuration, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        });
            
        await response.WriteAsync(configurationContent, token);
    }
    
    private static DialContext CreateContext(string deploymentName, HttpRequest request, HttpResponse _)
    {
        var apiKey = request.Headers.TryGetValue("Api-Key", out var apiKeyValues) ? apiKeyValues.FirstOrDefault() : null;
        
        if (apiKey is null)
            throw new Exception("Api-Key header is required.");
        
        return new DialContext(deploymentName, apiKey);
    }
}