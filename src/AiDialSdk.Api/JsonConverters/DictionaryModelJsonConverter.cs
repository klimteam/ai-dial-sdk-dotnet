using System.Text.Json;
using System.Text.Json.Serialization;
using AiDialSdk.Api.JsonConverters.Internal;

namespace AiDialSdk.Api.JsonConverters;

public class DictionaryModelJsonConverter<T> : JsonConverter<T>
    where T : Dictionary<string, object?>, new()
{
    private const string TypePropertyName = "$type";
    private const string ValuePropertyName = "$value";
    
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var result = new T();

        using var doc = JsonDocument.ParseValue(ref reader);
        foreach (var prop in doc.RootElement.EnumerateObject())
        {
            var valueElement = prop.Value;
            object? value;

            if (valueElement.ValueKind == JsonValueKind.Object &&
                valueElement.TryGetProperty(TypePropertyName, out var typeProperty))
            {
                var typeName = typeProperty.GetString();
                var type = Type.GetType(typeName ?? "") ?? throw new JsonException($"Cannot resolve type '{typeName}'");
                var valueJson = valueElement.GetProperty(ValuePropertyName).GetRawText();
                value = JsonSerializer.Deserialize(valueJson, type, options);
            }
            else
            {
                value = valueElement.Deserialize<object>(options);
            }

            result[prop.Name] = value;
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var kvp in value)
        {
            writer.WritePropertyName(kvp.Key);

            if (kvp.Value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                var type = kvp.Value.GetType();

                if (IsSimpleType(type))
                {
                    JsonSerializer.Serialize(writer, kvp.Value, type, options);
                }
                else
                {
                    writer.WriteStartObject();
                    writer.WriteString(TypePropertyName, type.AssemblyQualifiedName);
                    writer.WritePropertyName(ValuePropertyName);
                    JsonSerializer.Serialize(writer, kvp.Value, type, options);
                    writer.WriteEndObject();
                }
            }
        }

        writer.WriteEndObject();
    }
    
    private static bool IsSimpleType(Type type)
    {
        return type.IsEnum || JsonSimpleTypes.Value.Contains(type);
    }
}