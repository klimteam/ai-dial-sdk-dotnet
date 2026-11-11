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

        if (root.TryGetProperty("dial:widget", out var widgetProperty) &&
            Enum.TryParse<AiDialWidgetType>(widgetProperty.GetString(), true, out var widgetType))
        {
            return widgetType switch
            {
                AiDialWidgetType.Buttons => JsonSerializer.Deserialize<AiDialButtons>(root.GetRawText(), options) ??
                                            throw new Exception($"Failed to deserialize {nameof(AiDialButtons)}"),
                AiDialWidgetType.Form => JsonSerializer.Deserialize<AiDialForm>(root.GetRawText(), options) ??
                                         throw new Exception($"Failed to deserialize {nameof(AiDialForm)}"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return JsonSerializer.Deserialize<AiDialInputControl>(root.GetRawText(), options) ??
               throw new Exception($"Failed to deserialize {nameof(AiDialInputControl)}");
    }

    public override void Write(Utf8JsonWriter writer, AiDialBaseControl value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}