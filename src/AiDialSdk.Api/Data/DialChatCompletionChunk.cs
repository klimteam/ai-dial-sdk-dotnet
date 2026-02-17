using AiDialSdk.Api.Extensions;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialChatCompletionChunk(
    string id, 
    int created, 
    string model, 
    string? serviceTier, 
    string systemFingerprint, 
    IReadOnlyList<DialDeltaChoice> choices,
    Usage? usage)
    : BaseChatCompletion<DialDeltaChoice>(id, created, model, serviceTier, systemFingerprint, choices, usage)
{
    public override string Object => "chat.completion.chunk";
    
    public DialChatCompletionChunk Combine(DialChatCompletionChunk other)
    {
        if (other.Id != Id)
            throw new InvalidOperationException("Cannot combine ChatCompletions with different Ids.");
        
        if (other.Created != Created)
            throw new InvalidOperationException("Cannot combine ChatCompletions with different Created timestamps.");
        
        if (other.Model != Model)
            throw new InvalidOperationException("Cannot combine ChatCompletions with different Models.");
        
        if (other.SystemFingerprint != SystemFingerprint)
            throw new InvalidOperationException("Cannot combine ChatCompletions with different SystemFingerprints.");

        var usage = Usage is not null 
            ? other.Usage is not null 
                ? Usage.Combine(other.Usage)
                : Usage
            : other.Usage;
        
        return new DialChatCompletionChunk(
            Id,
            Created,
            Model,
            ServiceTier ?? other.ServiceTier,
            SystemFingerprint,
            Choices.Combine(other.Choices) ?? [],
            usage);
    }
}