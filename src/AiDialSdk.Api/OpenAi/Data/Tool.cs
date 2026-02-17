namespace AiDialSdk.Api.OpenAi.Data;

public class Tool
{
    public Tool(ToolType type, FunctionDefinition functionDefinition)
    {
        Type = type;
        FunctionDefinition = functionDefinition;
    }
    
    public ToolType Type { get; }
    
    public FunctionDefinition FunctionDefinition { get; }
}