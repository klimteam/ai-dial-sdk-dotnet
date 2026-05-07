using AiDialSdk.Api.Data;

namespace AiDialSdk.Api.Conversations;

public interface IDialConversationApiClient
{
    Task<DialConversation> GetConversationAsync(string conversationId, CancellationToken token);
    
    Task<string> GetConversationAsJsonAsync(string conversationId, CancellationToken token);
}