namespace AiDialSdk.Api.OpenAi.Data;

public class DeveloperMessage(string content, string? name = null) : BaseMessage(Role.Developer)
{
    public string Content { get; } = content;

    public string? Name { get; } = name;
}