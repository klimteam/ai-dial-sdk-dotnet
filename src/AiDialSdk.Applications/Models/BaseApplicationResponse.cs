using System.Text.Json;
using AiDialSdk.Api.Infrastructure;
using AiDialSdk.Api.OpenAi.Data;
using Microsoft.AspNetCore.Http;

namespace AiDialSdk.Applications.Models;

public abstract class BaseApplicationResponse
{
    protected const string SystemFingerprint = "ab_01";
    
    protected ChatCompletionRequest ChatCompletionRequest { get; private set; }
    protected HttpResponse HttpResponse { get; }

    protected string Id { get; }
    
    protected int Created { get; }
    
    protected string Model { get; }
    
    protected BaseApplicationResponse(string id, int created, string model, ChatCompletionRequest chatCompletionRequest, HttpResponse httpResponse)
    {
        Id = id;
        Created = created;
        Model = model;
        ChatCompletionRequest = chatCompletionRequest;
        HttpResponse = httpResponse;
    }
    
    protected string Serialize<T>(T data)
    {
        return JsonSerializer.Serialize(data, GlobalJsonSettings.ChatCompletionResponseJsonSerializerOptions);
    }
}