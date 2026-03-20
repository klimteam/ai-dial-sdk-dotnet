namespace AiDialSdk.Api.Conversations;

public interface IDialConversationApiClient
{
    Task GetConversationAsync(string conversationId, CancellationToken token);
}