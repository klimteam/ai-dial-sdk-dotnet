using System.Text.Json;
using AiDialSdk.Api.Data;

namespace AiDialSdk.Api.Files.Extensions;

public static class FileClientExtensions
{
    public static async Task<string> GetDataAsStringAsync(this IDialFileApiClient dialFileApiClient, string filaPath,
        CancellationToken token)
    {
        var stream = await dialFileApiClient.GetDataAsync(filaPath, token);
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync(token);
    }
    
    public static async Task<PutFileResponse> StoreDataAsJsonAsync<TData>(this IDialFileApiClient dialFileApiClient, 
        string fileName, TData data, JsonSerializerOptions jsonSerializerOptions, CancellationToken token)
    {
        var stringData = JsonSerializer.Serialize(data, jsonSerializerOptions);
        return await dialFileApiClient.StoreDataAsync(fileName, stringData, token);
    }
}