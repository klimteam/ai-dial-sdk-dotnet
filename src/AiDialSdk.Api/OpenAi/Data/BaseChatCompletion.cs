using System.Text.Json.Serialization;

namespace AiDialSdk.Api.OpenAi.Data;

public abstract class BaseChatCompletion<TChoice>(
    string id,
    int created,
    string model,
    string? serviceTier,
    string systemFingerprint,
    IReadOnlyList<TChoice> choices,
    Usage? usage)
{
    public string Id { get; } = id;

    public int Created { get; } = created;

    public string Model { get; } = model;
    
    public string? ServiceTier { get; } = serviceTier;
    
    public string SystemFingerprint { get; } = systemFingerprint;

    public abstract string Object { get; }
    
    public IReadOnlyList<TChoice> Choices { get; } = choices;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Usage? Usage { get; } = usage;
}