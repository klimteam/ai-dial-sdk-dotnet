using System.Text.Json.Serialization;
using AiDialSdk.Api.Extensions;

namespace AiDialSdk.Api.OpenAi.Data;

public abstract class CompletionChoice<TMessage> : BaseChoice, ICombinableCollectionItem<CompletionChoice<TMessage>>
    where TMessage : ChoiceCompletionMessage
{
    protected CompletionChoice(int index, TMessage message, object? logProbability, FinishReason finishReason) 
        : base(index, logProbability)
    {
        Message = message;
        FinishReason = finishReason;
    }
    
    [JsonPropertyOrder(2)]
    public TMessage Message { get; }
    
    [JsonPropertyOrder(4)]
    public FinishReason FinishReason { get; }

    public abstract CompletionChoice<TMessage> Combine(CompletionChoice<TMessage> other);
}