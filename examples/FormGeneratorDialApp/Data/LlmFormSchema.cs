using System.Text.Json.Serialization;

namespace FormGeneratorDialApp.Data;

public sealed class LlmFormSchema
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("chatMessageInputDisabled")]
    public bool ChatMessageInputDisabled { get; set; }

    [JsonPropertyName("fields")]
    public List<LlmFormField> Fields { get; set; } = [];
}