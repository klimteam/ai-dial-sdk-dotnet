using System.Text.Json.Serialization;

namespace AiDialSdk.Api.OpenAi.Data;

[JsonDerivedType(typeof(ResponseFormatText))]
[JsonDerivedType(typeof(ResponseFormatJsonObject))]
[JsonDerivedType(typeof(ResponseFormatJsonSchema))]
public abstract class ResponseFormat
{
    public abstract ResponseFormatType Type { get; }
}