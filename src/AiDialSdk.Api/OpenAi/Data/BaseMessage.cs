namespace AiDialSdk.Api.OpenAi.Data;

public abstract class BaseMessage(Role role)
{
    public Role Role { get; } = role;
}