namespace AiDialSdk.Api.OpenAi.Data;

public class ToolCall
{
    public ToolCall(int index, string id, ToolType type, Function function)
    {
        Index = index;
        Id = id;
        Type = type;
        Function = function;
    }
    
    public int Index { get; }
    
    public string Id { get; }
    
    public ToolType Type { get; }
    
    public Function Function { get; }
}