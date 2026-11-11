using AiDialSdk.Api.Clients;
using AiDialSdk.Applications.Implementations;
using AiDialSdk.Applications.Infrastructures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiDialSdk.Applications;

public static class SdkExtensions
{
    public static WebApplicationBuilder AddApplicationDialApiClient(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();
        builder.Services.Configure<DialApplicationConfiguration>(builder.Configuration.GetSection(nameof(DialApplicationConfiguration)));
        builder.Services.AddScoped<IDialApiClientFactory, DialApplicationApiClientFactory<BaseContext>>();
        builder.Services.AddScoped<IDialApiClient>(sp => sp.GetRequiredService<IDialApiClientFactory>().CreateDialApiClient());
        return builder;
    }
    
    public static WebApplicationBuilder AddDialApplication<TDialApplication>(this WebApplicationBuilder builder)
        where TDialApplication : class, IDialApplication
    {
        builder.Services.AddScoped<IDialContextAccessor<BaseContext>, DialContextAccessor<BaseContext>>();
        
        builder.Services.AddScoped<IDialRequestDeserializer, DialRequestDeserializer>();
        builder.Services.AddScoped<ICompletionInvoker, CompletionInvoker>();
        builder.Services.AddScoped<IConfigurationInvoker, ConfigurationInvoker>();

        builder.Services.AddScoped<IDialApplication, TDialApplication>();
        
        return builder;
    }
    
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