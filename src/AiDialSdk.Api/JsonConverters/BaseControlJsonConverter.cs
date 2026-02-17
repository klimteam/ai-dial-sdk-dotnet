using System.Text.Json;
using System.Text.Json.Serialization;
using AiDialSdk.Api.Data;

namespace AiDialSdk.Api.JsonConverters;

public class BaseControlJsonConverter : JsonConverter<AiDialBaseControl>
{
    public override AiDialBaseControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonMessage = JsonDocument.ParseValue(ref reader);
        var root = jsonMessage.RootElement;

        if (Enum.TryParse<AiDialWidgetType>(root.GetProperty("dial:widget").GetString(), true, out var widgetType))
        {
            return widgetType switch
            {
                AiDialWidgetType.Buttons => JsonSerializer.Deserialize<AiDialButton>(root.GetRawText(), options) ??
                                            throw new Exception($"Failed to deserialize {nameof(AiDialButton)}"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        throw new Exception("Failed to deserialize BaseControl");
    }

    public override void Write(Utf8JsonWriter writer, AiDialBaseControl value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}