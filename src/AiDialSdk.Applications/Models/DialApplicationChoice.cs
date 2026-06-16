using AiDialSdk.Api.Data;

namespace AiDialSdk.Applications.Models;

public class DialApplicationChoice : ApplicationChoice
{
    private readonly List<DialApplicationStage> _stages;
    private readonly List<DialAttachment> _attachments;
    
    private readonly IApplicationResponse _applicationResponse;
    
    public DialApplicationChoice(int index, IApplicationResponse applicationResponse) 
        : base(index, applicationResponse)
    {
        _stages = [];
        _attachments = [];
        _applicationResponse = applicationResponse;
    }
    
    public async Task<DialApplicationStage> CreateStageAsync(string? name, CancellationToken token)
    {
        if (Closed)
            throw new Exception("Cannot start a stage on a closed choice.");
        
        if (!Opened)
            await OpenAsync(token);
        
        var stage = new DialApplicationStage(_stages.Count, name, this);

        _stages.Add(stage);

        await stage.OpenAsync(token);
        
        return stage;
    }

    public async Task AppendAttachmentAsync(string type, string title, string url, CancellationToken token)
    {
        if (Closed)
            throw new Exception("Cannot add attachment to a closed choice.");
        
        if (!Opened)
            await OpenAsync(token);
        
        var attachment = new DialAttachment(_attachments.Count, type, title, null, url, null, null);

        _attachments.Add(attachment);
        
        await AppendAttachmentDataAsync(attachment, token);
    }
    
    public async Task AppendDataAttachmentAsync(string type, string title, string data, CancellationToken token)
    {
        if (Closed)
            throw new Exception("Cannot add attachment to a closed choice.");
        
        if (!Opened)
            await OpenAsync(token);
        
        var attachment = new DialAttachment(_attachments.Count, type, title, data, null, null, null);

        _attachments.Add(attachment);
        
        await AppendAttachmentDataAsync(attachment, token);
    }
    
    public async Task AppendFormSchemaAsync(DialFormSchema formSchema, CancellationToken token)
    {
        if (Closed)
            throw new Exception("Cannot append form schema to a closed choice.");
        
        var dialCustomContent = new DialCustomContent(formSchema);
        
        var choiceCompletionMessage = new DialChoiceCompletionMessage(dialCustomContent);

        var deltaChoice = new DialDeltaChoice(Index, choiceCompletionMessage, null, null);

        await _applicationResponse.AppendDeltaChoiceAsync(deltaChoice, token);
    }

    public async Task AppendStateAsync(IReadOnlyDictionary<string, object?> data, CancellationToken token)
    {
        if (Closed)
            throw new Exception("Cannot add attachment to a closed choice.");
        
        if (!Opened)
            await OpenAsync(token);

        var state = new DialState(data);
        
        await AppendStateDataAsync(state, token);
    }
    
    internal async Task AppendStageDataAsync(DialStage dialStage, CancellationToken token)
    {
        if (Closed)
            throw new Exception("Cannot append stage to a closed choice.");
        
        var dialCustomContent = new DialCustomContent(dialStage);
        
        var choiceCompletionMessage = new DialChoiceCompletionMessage(dialCustomContent);

        var deltaChoice = new DialDeltaChoice(Index, choiceCompletionMessage, null, null);

        await _applicationResponse.AppendDeltaChoiceAsync(deltaChoice, token);
    }
    
    private async Task AppendAttachmentDataAsync(DialAttachment dialAttachment, CancellationToken token)
    {
        if (Closed)
            throw new Exception("Cannot append attachments to a closed choice.");
        
        var dialCustomContent = new DialCustomContent(dialAttachment);
        
        var choiceCompletionMessage = new DialChoiceCompletionMessage(dialCustomContent);

        var deltaChoice = new DialDeltaChoice(Index, choiceCompletionMessage, null, null);

        await _applicationResponse.AppendDeltaChoiceAsync(deltaChoice, token);
    }

    private async Task AppendStateDataAsync(DialState dialState, CancellationToken token)
    {
        if (Closed)
            throw new Exception("Cannot append state to a closed choice.");
        
        var dialCustomContent = new DialCustomContent(dialState);
        
        var choiceCompletionMessage = new DialChoiceCompletionMessage(dialCustomContent);
        
        var deltaChoice = new DialDeltaChoice(Index, choiceCompletionMessage, null, null);

        await _applicationResponse.AppendDeltaChoiceAsync(deltaChoice, token);
    }
    
    protected override async Task InternalCloseAsync(CancellationToken token)
    {
        foreach (var stage in _stages)
        {
            await stage.CloseAsync(token);
        }
    }
}