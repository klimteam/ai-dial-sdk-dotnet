using AiDialSdk.Api.Data;
using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Chat;

public interface IDialChatApiClient
{
    string DeploymentName { get; }
    
    Task<DialChatResponse> CompleteChatAsync(IEnumerable<BaseMessage> messages, DialChatOptions chatOptions, 
        CancellationToken token = default);
    
    IAsyncEnumerable<DialChatCompletionChunk> StreamChatCompletionsAsync(IEnumerable<BaseMessage> messages, 
        DialChatOptions chatOptions, CancellationToken token = default);
}