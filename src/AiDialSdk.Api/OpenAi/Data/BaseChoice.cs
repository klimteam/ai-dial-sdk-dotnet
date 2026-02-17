using System.Text.Json.Serialization;

namespace AiDialSdk.Api.OpenAi.Data;

[JsonDerivedType(typeof(DeltaChoice))]
[JsonDerivedType(typeof(CompletionChoice<>))]
public abstract class BaseChoice(int index, object? logProbability = null)
{
    [JsonPropertyOrder(1)]
    public int Index { get; } = index;
    
    // ReSharper disable once StringLiteralTypo
    [JsonPropertyName("logprobs")]
    [JsonPropertyOrder(3)]
    public object? LogProbability { get; } = logProbability;
}