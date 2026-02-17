namespace AiDialSdk.Api.OpenAi.Data;

public class ChatCompletionChunk(
    string id, 
    int created, 
    string model, 
    string? serviceTier, 
    string systemFingerprint, 
    IReadOnlyList<DeltaChoice> choices,
    Usage? usage) 
    : BaseChatCompletion<DeltaChoice>(id, created, model, serviceTier, systemFingerprint, choices, usage)
{
    public override string Object => "chat.completion.chunk";
}