using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialChatCompletion(
    string id,
    int created,
    string model,
    string? serviceTier, 
    string systemFingerprint,
    IReadOnlyList<DialChoice> choices,
    Usage? usage)
    : BaseChatCompletion<DialChoice>(id, created, model, serviceTier, systemFingerprint, choices, usage)
{
    public override string Object => "chat.completion";
}