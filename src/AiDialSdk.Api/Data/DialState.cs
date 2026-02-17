namespace AiDialSdk.Api.Data;

public class DialState : BaseDictionaryModel
{
    public DialState()
    {
    }
    
    public DialState(IReadOnlyDictionary<string, object?> initialState)
        : base(initialState)
    {
    }
    
    public DialState Combine(DialState other)
    {
        var combinedState = new DialState();

        foreach (var item in this)
        {
            combinedState[item.Key] = item.Value;
        }
        
        foreach (var item in other)
        {
            combinedState[item.Key] = item.Value;
        }

        return combinedState;
    }
}