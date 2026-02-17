using AiDialSdk.Api.Extensions;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialDeltaChoice(int index, DialChoiceCompletionMessage delta, object? logProbability, FinishReason? finishReason)
    : BaseDeltaChoice<DialChoiceCompletionMessage>(index, delta, logProbability, finishReason), ICombinableCollectionItem<DialDeltaChoice>
{
    public virtual DialDeltaChoice Combine(DialDeltaChoice other)
    {
        if (Index != other.Index)
            throw new Exception("Indexes must be equal");
        
        var delta = Delta.Combine(other.Delta);
        return new DialDeltaChoice(Index,
            delta,
            other.LogProbability ?? LogProbability, 
            other.FinishReason ?? FinishReason);
    }
}