using System.Text.Json;

namespace AiDialSdk.Api.OpenAi.Data;

public class FunctionDefinition
{
    public FunctionDefinition(string name, JsonElement? parameters = null, bool? strict = null, string? description = null)
    {
        Name = name;
        Parameters = parameters;
        Strict = strict;
        Description = description;
    }
    
    public string Name { get; }
    
    public JsonElement? Parameters { get; }
        
    public bool? Strict { get; }
    
    public string? Description { get; }
}