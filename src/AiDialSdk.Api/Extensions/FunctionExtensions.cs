using AiDialSdk.Api.Infrastructure;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Extensions;

public static class FunctionExtensions
{
    public static T DeserializeArguments<T>(this Function function)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(function.Arguments, GlobalJsonSettings.DefaultJsonSerializerOptions) 
               ?? throw new InvalidOperationException($"Failed to deserialize function arguments for function '{function.Name}'");
    }
}