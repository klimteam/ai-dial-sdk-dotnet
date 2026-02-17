using System.Text.Json;
using System.Text.Json.Serialization;

namespace AiDialSdk.Api.JsonConverters;

public class ObjectJsonConverter : JsonConverter<object>
{
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return reader.GetString();
            case JsonTokenType.Number:
                if (reader.TryGetInt32(out var intValue))
                    return intValue;
                if (reader.TryGetInt64(out var longValue))
                    return longValue;
                return reader.GetDouble();
            case JsonTokenType.True:
            case JsonTokenType.False:
                return reader.GetBoolean();
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.StartObject:
            case JsonTokenType.StartArray:
                using (var document = JsonDocument.ParseValue(ref reader))
                {
                    return document.RootElement.Clone();
                }
            default:
                throw new JsonException($"Unsupported token type: {reader.TokenType}");
        }
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}