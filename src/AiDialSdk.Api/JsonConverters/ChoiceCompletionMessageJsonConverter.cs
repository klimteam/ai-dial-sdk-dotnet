using System.Text.Json;
using System.Text.Json.Serialization;

namespace AiDialSdk.Api.JsonConverters;

public class ChoiceCompletionMessageJsonConverter<T> : JsonConverter<T>
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<T>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value is null)
            return;
        
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}

public class ChoiceCompletionMessageJsonConverter<TSrc, TDst> : JsonConverter<TSrc> where TDst : TSrc
{
    public override TSrc? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<TDst>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, TSrc value, JsonSerializerOptions options)
    {
        if (value is null)
            return;
        
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}