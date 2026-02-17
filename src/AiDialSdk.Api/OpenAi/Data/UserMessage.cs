namespace AiDialSdk.Api.OpenAi.Data;

public class UserMessage: BaseMessage
{
    public UserMessage(string content, string? name = null) : base(Role.User)
    {
        Content = content;
        Name = name;
    }
    
    public string Content { get; }

    public string? Name { get; }
}