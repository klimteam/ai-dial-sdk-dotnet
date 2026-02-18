using System.Text.Json;
using AiDialSdk.Api.Data;
using AiDialSdk.Api.Infrastructure;
using AiDialSdk.Api.OpenAi.Data;
using Microsoft.AspNetCore.Http;

namespace Models;

public class StreamApplicationResponse : BaseApplicationResponse, IApplicationResponse
{
    public StreamApplicationResponse(string id, int created, string model, ChatCompletionRequest chatCompletionRequest, HttpResponse httpResponse)
        : base(id, created, model, chatCompletionRequest, httpResponse)
    {
    }
    
    public async Task AppendDeltaChoiceAsync(DialDeltaChoice deltaChoice, CancellationToken token)
    {
        var chunk = CreateChunk(Id, Created, Model, deltaChoice);
        
        var chunkContent =  JsonSerializer.Serialize(chunk, GlobalJsonSettings.ChatCompletionResponseJsonSerializerOptions);
        
        await WriteChunkDataAsync(HttpResponse, chunkContent, token);
    }

    public Task AppendUsageAsync(Usage usage, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task DoneAsync(CancellationToken token)
    {
        var doneChunk = CreateDoneChunk();
        await WriteChunkDataAsync(HttpResponse, doneChunk, token);
    }
    
     private static DialChatCompletionChunk CreateChunk(string completionId, int created, string model, DialDeltaChoice deltaChoice)
     {
         return new DialChatCompletionChunk(
             completionId,
             created,
             model, 
             null, 
             SystemFingerprint,
             [deltaChoice],
             null);
     }

     private static async Task WriteChunkDataAsync(HttpResponse httpResponse, string data, CancellationToken token)
     {
         await httpResponse.WriteAsync($"data: {data}\n\n", token);
     }
     
     private static string CreateDoneChunk()
     {
         return "[DONE]";
     }
}