namespace AiDialSdk.Api.OpenAi.Data;

public class ChatCompletionRequest
{
    public ChatCompletionRequest(
        IReadOnlyList<BaseMessage> messages, 
        string model, 
        bool? store = null, 
        object? metadata = null, 
        float? frequencyPenalty = null,
        int? maxTokens = null, 
        int? maxPromptTokens  = null,
        int? maxCompletionTokens = null,
        int? n = null, 
        float? presencePenalty = null, 
        ResponseFormat? responseFormat = null, 
        int? seed = null, 
        string? serviceTier = null, 
        bool? stream = null,
        float? temperature = null, 
        float? topP = null, 
        ToolChoiceMode? toolChoice = null,
        IReadOnlyList<Tool>? tools = null, 
        bool? parallelToolCalls = null, 
        string? user = null)
    {
        Messages = messages;
        Model = model;
        Store = store;
        Metadata = metadata;
        FrequencyPenalty = frequencyPenalty;
        MaxTokens = maxTokens;
        MaxPromptTokens = maxPromptTokens;
        MaxCompletionTokens = maxCompletionTokens;
        N = n ?? 1;
        PresencePenalty = presencePenalty;
        ResponseFormat = responseFormat;
        Seed = seed;
        ServiceTier = serviceTier;
        Stream = stream;
        Temperature = temperature;
        TopP = topP;
        ToolChoice = toolChoice;
        Tools = tools;
        ParallelToolCalls = parallelToolCalls;
        User = user;
    }
    
    public IReadOnlyList<BaseMessage> Messages { get; }
    
    public string Model { get; }
    
    public bool? Store { get; }

    public object? Metadata { get; }

    public float? FrequencyPenalty { get; }
    
    public int? MaxTokens { get; }
    
    public int? MaxPromptTokens { get; }
    
    public int? MaxCompletionTokens { get; }

    public int? N { get; }
    
    public float? PresencePenalty { get; }
    
    public ResponseFormat? ResponseFormat { get; }
    
    public int? Seed { get; }
    
    public string? ServiceTier { get; }
    
    public bool? Stream { get; }
    
    public float? Temperature { get; }
    
    public float? TopP { get; }
    
    public ToolChoiceMode? ToolChoice { get; }
    
    public IReadOnlyList<Tool>? Tools { get; }
    
    public bool? ParallelToolCalls { get; }
    
    public string? User { get; }
}