using System.Runtime.CompilerServices;
using System.Text;
using AiDialSdk.Api.Data.Internal;

namespace AiDialSdk.Api.Extensions;

internal static class HttpClientExtensions
{
    internal static async IAsyncEnumerable<SseMessage> ReadSseStreamAsync(this HttpClient httpClient,
        HttpRequestMessage httpRequestMessage, [EnumeratorCancellation] CancellationToken token)
    {
        using var response = await httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, token);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(token);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        
        List<string> block = [];

        while (await reader.ReadLineAsync(token) is { } line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (block.Count <= 0) continue;
                
                yield return Deserialize(block);
                block = [];
            }
            else
            {
                block.Add(line);
            }
        }
    }
    
    private static SseMessage Deserialize(IReadOnlyList<string> block)
    {
        string? id = null;
        StringBuilder? data = null;
        string? @event = null;
        
        foreach (var line in block)
        {
            if (line.StartsWith("id:"))
            {
                id = line.Substring(3).Trim();
            }
            else if (line.StartsWith("data:"))
            {
                if (data is null)
                    data = new StringBuilder(line.Substring(5).Trim());
                else
                    data.Append(line.Substring(5).Trim());
            }
            else if (line.StartsWith("event:"))
            {
                @event = line.Substring(6).Trim();
            }
            else
            {
                throw new InvalidOperationException($"Unexpected line in SSE block: {line}");
            }
        }
        
        return new SseMessage(id, data?.ToString(), @event);
    }
}