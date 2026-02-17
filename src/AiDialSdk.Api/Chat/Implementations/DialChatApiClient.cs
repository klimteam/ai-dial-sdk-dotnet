using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using AiDialSdk.Api.Chat.Extensions;
using AiDialSdk.Api.Clients.Implementations;
using AiDialSdk.Api.Data;
using AiDialSdk.Api.Extensions;
using AiDialSdk.Api.Infrastructure;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Chat.Implementations;

public class DialChatApiClient : BaseApiClient, IDialChatApiClient
{
    private readonly string? _apiVersion;
    
    public DialChatApiClient(HttpClient httpClient, Uri endpoint, string? apiKey, string deploymentName, string? apiVersion) : base(httpClient, endpoint, apiKey)
    {
        DeploymentName = deploymentName;
        
        _apiVersion = apiVersion;
    }

    public string DeploymentName { get; }

    public async Task<DialChatResponse> CompleteChatAsync(IEnumerable<BaseMessage> messages, 
        DialChatOptions chatOptions, CancellationToken token = default)
    {
        var chatCompletionRequest = new DialChatCompletionRequest(messages.ToList(), DeploymentName, chatOptions);
        var chatCompletion = await CompleteChatAsync(chatCompletionRequest, token);
        
        var choice = chatCompletion.Choices.FirstOrDefault() 
                     ?? throw new Exception("Chat completion returned no choices.");

        return new DialChatResponse(
            chatCompletion.Id, 
            chatCompletion.Created, 
            chatCompletion.Model,
            chatCompletion.Usage,
            choice.Message.ToDialAssistantMessage());
    }
    
    public async IAsyncEnumerable<DialChatCompletionChunk> StreamChatCompletionsAsync(IEnumerable<BaseMessage> messages, 
        DialChatOptions chatOptions, [EnumeratorCancellation] CancellationToken token = default)
    {
        chatOptions.Stream = true;
        var chatCompletionRequest = new DialChatCompletionRequest(messages.ToList(), DeploymentName, chatOptions);
        await foreach (var chunk in StreamChatCompletionsAsync(chatCompletionRequest, token))
        {
            yield return chunk;
        }
    }
    
    private async Task<DialChatCompletion> CompleteChatAsync(ChatCompletionRequest chatCompletionRequest, CancellationToken token = default)
    {
        var uri = BuildChatCompletionsUri(Endpoint, DeploymentName, _apiVersion);
        var content = JsonSerializer.Serialize(chatCompletionRequest, GlobalJsonSettings.ChatCompletionRequestJsonSerializerOptions);
        
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };
        
        var response = await SendAsync(httpRequestMessage, token);
        
        var responseContent = await response.Content.ReadAsStringAsync(token);
        var chatCompletion = JsonSerializer.Deserialize<DialChatCompletion>(responseContent, GlobalJsonSettings.ChatCompletionResponseJsonSerializerOptions)
                             ?? throw new NullReferenceException("Failed to deserialize ChatCompletion response.");
        
        return chatCompletion;
    }
    
    private async IAsyncEnumerable<DialChatCompletionChunk> StreamChatCompletionsAsync(
        ChatCompletionRequest chatCompletionRequest, 
        [EnumeratorCancellation] CancellationToken token = default)
    {
        var uri = BuildChatCompletionsUri(Endpoint, DeploymentName, _apiVersion);
        var content = JsonSerializer.Serialize(chatCompletionRequest, GlobalJsonSettings.ChatCompletionRequestJsonSerializerOptions);
        
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };

        ConfigureRequestHeaders(httpRequestMessage);
        
        await foreach (var sseData in HttpClient.ReadSseStreamAsync(httpRequestMessage, token))
        {
            if (string.IsNullOrWhiteSpace(sseData.Data))
                continue;
            if (sseData.Data == "[DONE]")
                yield break;
            var chatCompletion = JsonSerializer.Deserialize<DialChatCompletionChunk>(sseData.Data, GlobalJsonSettings.ChatCompletionResponseJsonSerializerOptions);
            if (chatCompletion is not null)
                yield return chatCompletion;
        }
    }
    
    private static Uri BuildChatCompletionsUri(Uri endpoint, string deploymentName, string? apiVersion)
    {
        var uriBuilder = new StringBuilder(endpoint.ToString());
        
        if (uriBuilder[^1] != '/')
        {
            uriBuilder.Append('/');
        }
        
        uriBuilder.Append($"openai/deployments/{deploymentName}/chat/completions");
        if (!string.IsNullOrWhiteSpace(apiVersion))
        {
            uriBuilder.Append($"?api-version={apiVersion}");
        }
        return new Uri(uriBuilder.ToString());
    }
}