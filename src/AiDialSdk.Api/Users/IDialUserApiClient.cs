using AiDialSdk.Api.Data;

namespace AiDialSdk.Api.Users;

public interface IDialUserApiClient
{
    Task<UserInfo> GetUserInfoAsync(CancellationToken token);
}