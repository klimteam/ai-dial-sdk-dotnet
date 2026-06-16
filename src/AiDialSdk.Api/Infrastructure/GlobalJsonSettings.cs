using System.Text.Json;
using System.Text.Json.Serialization;
using AiDialSdk.Api.Data;
using AiDialSdk.Api.JsonConverters;

namespace AiDialSdk.Api.Infrastructure;

public static class GlobalJsonSettings
{
    private static readonly JsonNamingPolicy DefaultPropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    private static readonly JsonNamingPolicy ConversationsPropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    
    public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = DefaultPropertyNamingPolicy,
        Converters =
        {
            new JsonStringEnumConverter(DefaultPropertyNamingPolicy)
        },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    
    public static readonly JsonSerializerOptions DefaultConversationsJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = ConversationsPropertyNamingPolicy,
        Converters =
        {
            new JsonStringEnumConverter(ConversationsPropertyNamingPolicy)
        },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    
    public static readonly JsonSerializerOptions ChatCompletionRequestJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = ConversationsPropertyNamingPolicy,
        Converters =
        {
            new MessageJsonConverter(),
            new DictionaryModelJsonConverter<DialState>(),
            new DictionaryModelJsonConverter<DialConfigurationValues>(),
            new DictionaryModelJsonConverter<DialFormValue>(),
            new JsonStringEnumConverter(ConversationsPropertyNamingPolicy)
        },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    
    public static readonly JsonSerializerOptions ChatCompletionResponseJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = ConversationsPropertyNamingPolicy,
        Converters =
        {
            new DialChoiceCompletionMessageJsonConverter(),
            new MessageJsonConverter(),
            new ObjectJsonConverter(),
            new DictionaryModelJsonConverter<DialState>(),
            new JsonStringEnumConverter(ConversationsPropertyNamingPolicy)
        },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}