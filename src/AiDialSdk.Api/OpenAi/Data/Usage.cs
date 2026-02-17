using AiDialSdk.Api.Extensions;

namespace AiDialSdk.Api.OpenAi.Data;

public class Usage(int promptTokens, int completionTokens, int totalTokens) : ICombinable<Usage>
{
    public int PromptTokens { get; } = promptTokens;
    
    public int CompletionTokens { get; } = completionTokens;

    public int TotalTokens { get; } = totalTokens;

    public Usage Combine(Usage other)
    {
        return new Usage(
            PromptTokens + other.PromptTokens,
            CompletionTokens + other.CompletionTokens,
            TotalTokens + other.TotalTokens);
    }
}