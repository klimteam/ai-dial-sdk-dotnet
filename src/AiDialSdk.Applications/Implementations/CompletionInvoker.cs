using AiDialSdk.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Models;

namespace Implementations;

public class CompletionInvoker : ICompletionInvoker
{
    private readonly IDialRequestDeserializer _requestDeserializer;
    private readonly IDialContextAccessor<BaseContext> _dialContextAccessor;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CompletionInvoker> _logger;

    public CompletionInvoker(
        IDialRequestDeserializer requestDeserializer,
        IDialContextAccessor<BaseContext> dialContextAccessor,
        IServiceProvider serviceProvider,
        ILogger<CompletionInvoker> logger)
    {
        _requestDeserializer = requestDeserializer;
        _dialContextAccessor = dialContextAccessor;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public async Task InvokeAsync(string deploymentName, HttpRequest request, HttpResponse response, CancellationToken token)
    {
        var chatCompletionRequest = await _requestDeserializer.DeserializeAsync(request, token);
        
        if (chatCompletionRequest.Stream ?? false)
        {
            response.Headers.ContentType = new StringValues("text/event-stream");
        }

        var completionContext = CreateChoiceCompletionContext(deploymentName, chatCompletionRequest, request, response);
        
        if (_dialContextAccessor is IDialContextSetter<BaseContext> setter)
        {
            setter.SetCompletionContext(completionContext);
        }
        
        var application = _serviceProvider.GetRequiredService<IDialApplication>();

        try
        {
            using var loggerScope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["ConversationId"] = completionContext.ConversationId ?? "null",
                ["RequestId"] = Guid.NewGuid()
            });
            
            await application.CompleteChoicesAsync(completionContext, token);
            
            await completionContext.DoneAsync(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during completion");
            throw;
        }
    }
    
    private static DialChoiceCompletionContext CreateChoiceCompletionContext(string deploymentName,
        DialChatCompletionRequest chatCompletionRequest, HttpRequest request, HttpResponse response)
    {
        if (chatCompletionRequest.N > 1)
            throw new Exception("N must be 1 for single choice completion context.");
        
        var apiKey = request.Headers.TryGetValue("Api-Key", out var apiKeyValues) ? apiKeyValues.FirstOrDefault() : null;

        if (apiKey is null)
            throw new Exception("Api-Key header is required.");
        
        var conversationId = request.Headers.TryGetValue("x-conversation-id", out var conversationIdValues) ? conversationIdValues.FirstOrDefault() : null;
        
        var authorization = request.Headers.TryGetValue("Authorization", out var authenticationValues) ? authenticationValues.FirstOrDefault() : null;
        
        var id = Guid.NewGuid().ToString();
        var created = (int)TimeProvider.System.GetUtcNow().ToUnixTimeSeconds();
    
        IApplicationResponse applicationResponse = chatCompletionRequest.Stream ?? false
            ? new StreamApplicationResponse(id, created, chatCompletionRequest.Model, chatCompletionRequest, response)
            : new ApplicationResponse(id, created, chatCompletionRequest.Model, chatCompletionRequest, response);

        var choices = new List<DialApplicationChoice>(chatCompletionRequest.N ?? 1);
        
        var n = chatCompletionRequest.N ?? 1;
        for (int i = 0; i < n; i++)
        {
            choices.Add(new DialApplicationChoice(i, applicationResponse));
        }
    
        return new DialChoiceCompletionContext(
            conversationId, 
            deploymentName, 
            apiKey,
            authorization,
            chatCompletionRequest,
            applicationResponse,
            choices);
    }
}