using System.Text.Json;

namespace AiDialSdk.Api.OpenAi.Data;

public class JsonSchema
{
    public JsonSchema(JsonElement schema, string description, string name, bool strict = false)
    {
        Schema = schema;
        Description = description;
        Name = name;
        Strict = strict;
    }

    public string Description { get; }
    
    public string Name { get; }
    
    public JsonElement Schema { get; }
    
    public bool Strict { get; }
}