namespace AiDialSdk.Api.Data;

public class UserInfo
{
    public UserInfo(IReadOnlyList<string>? roles, IReadOnlyDictionary<string, IReadOnlyList<string>>? userClaims)
    {
        Roles = roles ?? [];
        UserClaims = userClaims ?? new Dictionary<string, IReadOnlyList<string>>();
    }

    public IReadOnlyList<string> Roles { get; }
    
    public IReadOnlyDictionary<string, IReadOnlyList<string>> UserClaims { get; }
}