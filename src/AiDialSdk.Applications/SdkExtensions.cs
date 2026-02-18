using AiDialSdk.Api.Clients;
using Implementations;
using Infrastructures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class SdkExtensions
{
    public static IServiceCollection AddApplicationDialApiClient(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        services.AddHttpClient();
        services.Configure<DialApplicationConfiguration>(configurationManager.GetSection(nameof(DialApplicationConfiguration)));
        services.AddScoped<IDialApiClientFactory, DialApplicationApiClientFactory<BaseContext>>();
        services.AddScoped<IDialApiClient>(sp => sp.GetRequiredService<IDialApiClientFactory>().CreateDialApiClient());
        return services;
    }
    
    public static IServiceCollection AddDialApplication<TDialApplication>(this IServiceCollection services)
        where TDialApplication : class, IDialApplication
    {
        services.AddScoped<IDialContextAccessor<BaseContext>, DialContextAccessor<BaseContext>>();
        
        services.AddScoped<IDialRequestDeserializer, DialRequestDeserializer>();
        services.AddScoped<ICompletionInvoker, CompletionInvoker>();
        services.AddScoped<IConfigurationInvoker, ConfigurationInvoker>();

        services.AddScoped<IDialApplication, TDialApplication>();
        
        return services;
    }
    
    public static WebApplication AppendDialAppMiddlewares(this WebApplication app)
    {
        app.MapGet("/openai/deployments/{deploymentName}/configuration", async (
            [FromRoute] string deploymentName,
            HttpRequest request,
            HttpResponse response,
            IConfigurationInvoker configurationInvoker,
            CancellationToken token) =>
        {
            await configurationInvoker.InvokeAsync(deploymentName, request, response, token);
        }).AllowAnonymous();
        
        app.MapPost("/openai/deployments/{deploymentName}/chat/completions", async (
            [FromRoute] string deploymentName,
            HttpRequest request,
            HttpResponse response,
            ICompletionInvoker completionInvoker,
            CancellationToken ct) =>
        {
            await completionInvoker.InvokeAsync(deploymentName, request, response, ct);
        }).AllowAnonymous();
        
        return app;
    }
}