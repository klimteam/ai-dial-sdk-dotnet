using AiDialSdk.Api.Chat;
using AiDialSdk.Api.Conversations;
using AiDialSdk.Api.Files;
using AiDialSdk.Api.Users;

namespace AiDialSdk.Api.Clients;

public interface IDialApiClient
{
    IDialChatApiClient GetChatClient(string deploymentName, string? apiVersion = null);
    IDialUserApiClient GetUserApiClient();
    IDialFileApiClient GetFileApiClient();
    IDialConversationApiClient GetConversationApiClient();
}