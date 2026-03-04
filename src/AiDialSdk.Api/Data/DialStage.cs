using AiDialSdk.Api.Extensions;

namespace AiDialSdk.Api.Data;

public class DialStage : ICombinableCollectionItem<DialStage>
{
    public DialStage(int index, string? name, DialStageStatus? status, string? content, IReadOnlyList<DialAttachment>? attachments)
    {
        Index = index;
        Name = name;
        Status = status;
        Content = content;
        Attachments = attachments;
    }
    public int Index { get; }
    
    public string? Name { get; }
    
    public DialStageStatus? Status { get; }
    
    public string? Content { get; }
    
    public IReadOnlyList<DialAttachment>? Attachments { get; }
    
    public DialStage Combine(DialStage other)
    {
        if (Index != other.Index)
            throw new Exception("Indexes must be equal");

        return new DialStage(
            Index,
            other.Name ?? Name,
            other.Status ?? Status,
            other.Content ?? Content,
            Attachments.Combine(other.Attachments));
    }
}