using AiDialSdk.Api.Extensions;

namespace AiDialSdk.Api.OpenAi.Data;

public class ChatCompletion : BaseChatCompletion<Choice>, ICombinable<ChatCompletion>
{
    public ChatCompletion(
        string id, 
        int created, 
        string model, 
        string? serviceTier, 
        string systemFingerprint, 
        IReadOnlyList<Choice> choices,
        Usage? usage) 
        : base(id, created, model, serviceTier, systemFingerprint, choices, usage)
    {
    }
    
    public override string Object => "chat.completion";
    
    public ChatCompletion Combine(ChatCompletion other)
    {
        if (other.Id != Id)
            throw new InvalidOperationException("Cannot combine ChatCompletions with different Ids.");
        
        if (other.Created != Created)
            throw new InvalidOperationException("Cannot combine ChatCompletions with different Created timestamps.");
        
        if (other.Model != Model)
            throw new InvalidOperationException("Cannot combine ChatCompletions with different Models.");
        
        if (other.SystemFingerprint != SystemFingerprint)
            throw new InvalidOperationException("Cannot combine ChatCompletions with different SystemFingerprints.");

        return new ChatCompletion(
            Id, 
            Created, 
            Model, 
            ServiceTier ?? other.ServiceTier, 
            SystemFingerprint, 
            Choices.Combine(other.Choices) ?? [], 
            Usage);
    }
}