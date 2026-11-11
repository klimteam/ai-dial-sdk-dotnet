using System.Text.Json;

namespace AiDialSdk.Api.OpenAi.Data;

public class ResponseFormatJsonSchema : ResponseFormat
{
    public ResponseFormatJsonSchema(JsonSchema jsonSchema)
    {
        JsonSchema = jsonSchema;
    }
    
    public JsonSchema JsonSchema { get; }
    
    public override ResponseFormatType Type => ResponseFormatType.JsonSchema;
}