using System.Text.Json;
using AiDialSdk.Api.Data;

namespace AiDialSdk.Api.Files.Extensions;

public static class FileClientExtensions
{
    public static async Task<PutFileResponse> StoreDataAsJsonAsync<TData>(this IDialFileApiClient dialFileApiClient, 
        string fileName, TData data, JsonSerializerOptions jsonSerializerOptions, CancellationToken token)
    {
        var stringData = JsonSerializer.Serialize(data, jsonSerializerOptions);
        return await dialFileApiClient.StoreDataAsync(fileName, stringData, token);
    }
}