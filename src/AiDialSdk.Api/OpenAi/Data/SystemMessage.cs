namespace AiDialSdk.Api.OpenAi.Data;

public class SystemMessage(string content, string? name = null) : BaseMessage(Role.System)
{
    public string Content { get; } = content;

    public string? Name { get; } = name;
}