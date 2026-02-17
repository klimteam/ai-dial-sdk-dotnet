using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialChoice : CompletionChoice<DialChoiceCompletionMessage>
{
    public DialChoice(int index, DialChoiceCompletionMessage message, object? logProbability, FinishReason finishReason) 
        : base(index, message, logProbability, finishReason)
    {
    }

    public override CompletionChoice<DialChoiceCompletionMessage> Combine(CompletionChoice<DialChoiceCompletionMessage> other)
    {
        if (Index != other.Index)
            throw new Exception("Indexes must be equal");
        
        var message = Message.Combine(other.Message);
        return new DialChoice(Index, message, other.LogProbability ?? LogProbability, other.FinishReason);
    }
    
    public DialChoice Combine(DialDeltaChoice deltaChoice)
    {
        if (Index != deltaChoice.Index)
            throw new Exception("Indexes must be equal");

        var message = Message.Combine(deltaChoice.Delta);
        return new DialChoice(
            Index,
            message,
            deltaChoice.LogProbability ?? LogProbability, 
            deltaChoice.FinishReason ?? FinishReason);
    }
}