using AiDialSdk.Api.Data;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Applications.Models;

public class ApplicationChoice
{
    private readonly IApplicationResponse _applicationResponse;
    
    protected int Index { get; }

    protected bool Opened { get; private set; }
    
    protected bool Closed { get; private set; }

    public ApplicationChoice(int index, IApplicationResponse applicationResponse)
    {
        Index = index;
        
        _applicationResponse = applicationResponse;
    }
    
    public async Task AppendContentAsync(string content, CancellationToken token)
    {
        if (!Opened)
            await OpenAsync(token);

        await _applicationResponse.AppendDeltaChoiceAsync(CreateContentDeltaChoice(content), token);
    }
    
    public async Task OpenAsync(CancellationToken token)
    {
        if (Closed)
            throw new Exception("Cannot open a closed stage.");
        
        if (Opened)
            return;
        
        await _applicationResponse.AppendDeltaChoiceAsync(CreateStartDeltaChoice(), token);

        Opened = true;
    }

    public async Task CloseAsync(CancellationToken token)
    {
        await CloseAsync(null, token);
    }
    
    public async Task CloseAsync(string? refusal, CancellationToken token)
    {
        if (Closed)
            return;

        if (!Opened)
            await OpenAsync(token);

        await InternalCloseAsync(token);
        
        await _applicationResponse.AppendDeltaChoiceAsync(CreateFinishDeltaChoice(refusal), token);
        
        Closed = true;
    }
    
    protected virtual Task InternalCloseAsync(CancellationToken token)
    {
        return Task.CompletedTask;
    }

    private DialDeltaChoice CreateStartDeltaChoice()
    {
        var choiceCompletionMessage = CreateChoiceCompletionRequest(Role.Assistant, string.Empty, null, null, null);
        return new DialDeltaChoice(Index, choiceCompletionMessage, null, null);
    }

    private DialDeltaChoice CreateContentDeltaChoice(string content)
    {
        var choiceCompletionMessage = CreateChoiceCompletionRequest(Role.Assistant, content, null, null, null);
        return new DialDeltaChoice(Index, choiceCompletionMessage, null, null);
    }
    
    private DialDeltaChoice CreateFinishDeltaChoice(string? refusal = null)
    {
        var choiceCompletionMessage = CreateChoiceCompletionRequest(Role.Assistant, null, refusal, null, null);
        return new DialDeltaChoice(Index, choiceCompletionMessage, null, FinishReason.Stop);
    }

    protected virtual DialChoiceCompletionMessage CreateChoiceCompletionRequest(Role? role, string? content, 
        string? refusal, IReadOnlyList<ToolCall>? toolCalls, DialCustomContent? customContent)
    {
        return new DialChoiceCompletionMessage(Role.Assistant, content, refusal, toolCalls, customContent);
    }
}