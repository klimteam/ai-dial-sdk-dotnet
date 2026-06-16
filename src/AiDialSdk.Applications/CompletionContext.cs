using AiDialSdk.Api.Data;
using AiDialSdk.Api.OpenAi.Data;
using AiDialSdk.Applications.Models;

namespace AiDialSdk.Applications;

public abstract class BaseContext
{
    protected BaseContext(string deploymentName, string apiKey)
    {
        DeploymentName = deploymentName;
        ApiKey = apiKey;
    }
    
    public string DeploymentName { get; }
    
    public string ApiKey { get; }
}

public class DialContext : BaseContext
{
    public DialContext(string deploymentName, string apiKey) : base(deploymentName, apiKey)
    {
    }
}

public abstract class BaseCompletionContext : BaseContext
{
    protected BaseCompletionContext(string? conversationId, string deploymentName, string apiKey, ChatCompletionRequest chatCompletionRequest)
        : base(deploymentName, apiKey)
    {
        ConversationId = conversationId;
        ChatCompletionRequest = chatCompletionRequest;
    }
    
    public string? ConversationId { get; }
    
    public ChatCompletionRequest ChatCompletionRequest { get; }

    public abstract Task DoneAsync(CancellationToken token);
    
    protected abstract Task FinishAsync(CancellationToken token);
}

public abstract class BaseCompletionContext<TApplicationResponse, TApplicationChoice> : BaseCompletionContext
    where TApplicationChoice : ApplicationChoice
    where TApplicationResponse : IApplicationResponse 
{
    public string? AuthToken { get; }
    
    public TApplicationResponse Response { get; }

    public IReadOnlyList<TApplicationChoice> Choices { get; }
    
    protected BaseCompletionContext(
        string? conversationId,
        string deploymentName,
        string apiKey,
        string? authToken,
        ChatCompletionRequest chatCompletionRequest,
        TApplicationResponse response,
        IReadOnlyList<TApplicationChoice> choices) 
        : base(conversationId, deploymentName, apiKey, chatCompletionRequest)
    {
        AuthToken = authToken;
        Response = response;
        Choices = choices;
    }
    
    public override async Task DoneAsync(CancellationToken token)
    {
        await FinishAsync(token);
        await Response.DoneAsync(token);
    }
    
    protected override async Task FinishAsync(CancellationToken token)
    {
        foreach (var choice in Choices)
        {
            await choice.CloseAsync(token);
        }
    }
}

public class DialChoiceCompletionContext : BaseCompletionContext<IApplicationResponse, DialApplicationChoice>
{
    public DialChoiceCompletionContext(
        string? conversationId,
        string deploymentName, 
        string apiKey,
        string? authToken,
        DialChatCompletionRequest chatCompletionRequest,
        IApplicationResponse response, 
        IReadOnlyList<DialApplicationChoice> choices)
        : base(conversationId, deploymentName, apiKey, authToken, chatCompletionRequest, response, choices)
    {
    }
}