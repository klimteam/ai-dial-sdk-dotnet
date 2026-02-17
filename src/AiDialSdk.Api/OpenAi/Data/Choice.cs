namespace AiDialSdk.Api.OpenAi.Data;

public class Choice : CompletionChoice<ChoiceCompletionMessage>
{
    public Choice(int index, ChoiceCompletionMessage message, object? logProbability, FinishReason finishReason) 
        : base(index, message, logProbability, finishReason)
    {
    }
    
    public override CompletionChoice<ChoiceCompletionMessage> Combine(CompletionChoice<ChoiceCompletionMessage> other)
    {
        if (Index != other.Index)
            throw new Exception("Indexes must be equal");
        
        var message = Message.Combine(other.Message);
        return new Choice(Index, message, other.LogProbability ?? LogProbability, other.FinishReason);
    }

    public CompletionChoice<ChoiceCompletionMessage> Combine(DeltaChoice deltaChoice)
    {
        if (Index != deltaChoice.Index)
            throw new Exception("Indexes must be equal");

        var message = Message.Combine(deltaChoice.Delta);
        return new Choice(Index, 
            message,
            deltaChoice.LogProbability ?? LogProbability, 
            deltaChoice.FinishReason ?? FinishReason);
    }
}