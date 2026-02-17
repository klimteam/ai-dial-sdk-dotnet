using System.Text.Json;
using System.Text.Json.Serialization;
using AiDialSdk.Api.Infrastructure;

namespace AiDialSdk.Api.JsonConverters;

public class DefaultJsonConverter<T> : JsonConverter<T>
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => JsonSerializer.Deserialize<T>(ref reader, GlobalJsonSettings.DefaultJsonSerializerOptions);

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, value, GlobalJsonSettings.DefaultJsonSerializerOptions);
}