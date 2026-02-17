using System.Text.Json;
using System.Text.Json.Serialization;
using AiDialSdk.Api.Data;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.JsonConverters;

public class DialChoiceCompletionMessageJsonConverter : JsonConverter<ChoiceCompletionMessage>
{
    public override ChoiceCompletionMessage? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<DialChoiceCompletionMessage>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, ChoiceCompletionMessage value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}