using System.Text.Json.Serialization;
using AiDialSdk.Api.Extensions;

namespace AiDialSdk.Api.OpenAi.Data;

public class BaseDeltaChoice<TDelta> : BaseChoice where TDelta : ChoiceCompletionMessage
{
    protected BaseDeltaChoice(int index, TDelta delta, object? logProbability, FinishReason? finishReason) : base(index)
    {
        Delta = delta;
        FinishReason = finishReason;
    }
    
    [JsonPropertyOrder(2)]
    public TDelta Delta { get; }
    
    [JsonPropertyOrder(4)]
    public FinishReason? FinishReason { get; }
}

public class DeltaChoice(int index, ChoiceCompletionMessage delta, object? logProbability, FinishReason? finishReason)
    : BaseDeltaChoice<ChoiceCompletionMessage>(index, delta, logProbability, finishReason), ICombinableCollectionItem<DeltaChoice>
{
    public virtual DeltaChoice Combine(DeltaChoice other)
    {
        if (Index != other.Index)
            throw new Exception("Indexes must be equal");
        
        var delta = Delta.Combine(other.Delta);
        return new DeltaChoice(Index,
            delta,
            other.LogProbability ?? LogProbability, 
            other.FinishReason ?? FinishReason);
    }
}