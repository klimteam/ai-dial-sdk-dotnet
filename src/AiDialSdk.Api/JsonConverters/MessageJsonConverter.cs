using System.Text.Json;
using System.Text.Json.Serialization;
using AiDialSdk.Api.Data;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.JsonConverters;

public class MessageJsonConverter : JsonConverter<BaseMessage>
{
    public override BaseMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonMessage = JsonDocument.ParseValue(ref reader);
        var root = jsonMessage.RootElement;

        if (!Enum.TryParse<Role>(root.GetProperty("role").GetString(), true, out var role))
            throw new Exception("Failed to deserialize BaseMessage");

        return role switch
        {
            Role.Assistant => JsonSerializer.Deserialize<DialAssistantMessage>(root.GetRawText(), options) ??
                              throw new Exception($"Failed to deserialize {nameof(DialAssistantMessage)}"),
            Role.System => JsonSerializer.Deserialize<SystemMessage>(root.GetRawText(), options) ??
                           throw new Exception($"Failed to deserialize {nameof(SystemMessage)}"),
            Role.Tool => JsonSerializer.Deserialize<DialToolMessage>(root.GetRawText(), options) ??
                         throw new Exception($"Failed to deserialize {nameof(ToolMessage)}"),
            Role.User => JsonSerializer.Deserialize<DialUserMessage>(root.GetRawText(), options) ??
                         throw new Exception($"Failed to deserialize {nameof(DialUserMessage)}"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override void Write(Utf8JsonWriter writer, BaseMessage value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize<object>(writer, value, options);
    }
}