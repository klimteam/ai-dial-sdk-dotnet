namespace AiDialSdk.Api.OpenAi.Data;

public class ToolMessage(string content, string toolCallId) : BaseMessage(Role.Tool)
{
    public string Content { get; } = content;

    public string ToolCallId { get; } = toolCallId;
}