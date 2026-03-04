using System.Text.Json;
using AiDialSdk.Api.Infrastructure;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Extensions;

public static class ToolMessageExtensions
{
    public static T GetResultAs<T>(this ToolMessage toolMessage)
    {
        return JsonSerializer.Deserialize<T>(toolMessage.Content, GlobalJsonSettings.DefaultJsonSerializerOptions) 
               ?? throw new InvalidOperationException($"Failed to deserialize tool message content for tool call with id '{toolMessage.ToolCallId}'");
    }
}