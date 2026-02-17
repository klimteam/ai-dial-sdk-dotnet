using System.Text.Json;

namespace AiDialSdk.Api.OpenAi.Data;

public class ResponseFormatJsonSchema : ResponseFormat
{
    public ResponseFormatJsonSchema(string name, JsonElement schema, string? description, bool? strict)
    {
        Name = name;
        Schema = schema;
        Description = description;
        Strict = strict;
    }
    
    public string Name { get; }
    
    public JsonElement Schema { get; }
    
    public string? Description { get; }
    
    public bool? Strict { get; }
    
    public override ResponseFormatType Type => ResponseFormatType.JsonSchema;
}