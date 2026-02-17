using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using AiDialSdk.Api.Infrastructure;

namespace AiDialSdk.Api.Data;

public class BaseDictionaryModel : Dictionary<string, object?>
{
    protected BaseDictionaryModel()
    {
    }
    
    protected BaseDictionaryModel(IReadOnlyDictionary<string, object?> initialState)
        : base(initialState)
    {
    }
    
    public bool TryGetValue<T>(string key, [MaybeNullWhen(false)] out T value)
    {
        if (!base.TryGetValue(key, out var obj))
        {
            value = default;
            return false;
        }

        if (obj is T typedValue)
        {
            value = typedValue;
            return true;
        }

        if (obj is JsonElement)
        {
            try
            {
                value = JsonSerializer.Deserialize<T>(obj.ToString()!, GlobalJsonSettings.DefaultConversationsJsonSerializerOptions);
                if (value is not null)
                    return true;
            }
            catch (JsonException)
            {
                value = default;
                return false;
            }
        }
            
        value = default;
        return false;
    }
}