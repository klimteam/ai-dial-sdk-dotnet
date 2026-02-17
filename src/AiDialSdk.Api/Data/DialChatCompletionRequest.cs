using System.Text.Json.Serialization;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialChatCompletionRequest : ChatCompletionRequest
{
    [JsonConstructor]
    public DialChatCompletionRequest(
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
        string? user = null,
        DialRequestCustomFields? customFields = null)
        : base(messages, model, store, metadata, frequencyPenalty, maxTokens, maxPromptTokens, maxCompletionTokens, n, 
            presencePenalty, responseFormat, seed, serviceTier, stream, temperature, topP, toolChoice, tools, 
            parallelToolCalls, user)
    {
        CustomFields = customFields;
    }

    public DialChatCompletionRequest(
        IReadOnlyList<BaseMessage> messages,
        string model,
        DialChatOptions chatOptions) 
        : base(messages, model, store: null, metadata: null, frequencyPenalty: null, 
            maxTokens: null, maxPromptTokens: null,
            maxCompletionTokens: chatOptions.MaxCompletionTokens, n: null, presencePenalty: null, 
            responseFormat: chatOptions.ResponseFormat, seed: chatOptions.Seed, serviceTier: null, 
            stream: chatOptions.Stream, temperature: chatOptions.Temperature, topP: chatOptions.TopP,
            toolChoice: chatOptions.ToolChoice, tools: chatOptions.Tools, parallelToolCalls: null, user: null)
    {
        CustomFields = chatOptions.CustomFields;
    }

    public DialRequestCustomFields? CustomFields { get; }
}