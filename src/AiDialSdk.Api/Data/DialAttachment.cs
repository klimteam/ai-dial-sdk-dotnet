using AiDialSdk.Api.Extensions;

namespace AiDialSdk.Api.Data;

public class DialAttachment : ICombinableCollectionItem<DialAttachment>
{
    public DialAttachment(
        int index, 
        string? type = null,
        string? title = null, 
        string? data = null, 
        string? url = null, 
        string? referenceUrl = null, 
        string? referenceType = null)
    {
        Index = index;
        Type = type;
        Title = title;
        Data = data;
        Url = url;
        ReferenceUrl = referenceUrl;
        ReferenceType = referenceType;
    }
    
    public int Index { get; }
    
    public string? Type { get; }
    
    public string? Title { get; }
    
    public string? Data { get; }
    
    public string? Url { get; }

    public string? ReferenceUrl { get; }
    
    public string? ReferenceType { get; }
    
    public DialAttachment Combine(DialAttachment other)
    {
        if (Index != other.Index)
            throw new Exception("Indexes must be equal");
        
        return new DialAttachment(
            Index,
            other.Type ?? Type,
            other.Title ?? Title,
            other.Data ?? Data,
            other.Url ?? Url,
            other.ReferenceUrl ?? ReferenceUrl,
            other.ReferenceType ?? ReferenceType);
    }
}