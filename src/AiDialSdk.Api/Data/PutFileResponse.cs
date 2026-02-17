namespace AiDialSdk.Api.Data;

public class PutFileResponse
{
    public PutFileResponse(string name, string? parentPath, string bucket, string url, string nodeType, string resourceType, long createdAt, long updatedAt, string etag, long contentLength, string contentType)
    {
        Name = name;
        ParentPath = parentPath;
        Bucket = bucket;
        Url = url;
        NodeType = nodeType;
        ResourceType = resourceType;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Etag = etag;
        ContentLength = contentLength;
        ContentType = contentType;
    }
    
    public string Name { get; }
    public string? ParentPath { get; }
    public string Bucket { get; }
    public string Url { get; }
    public string NodeType { get; }
    public string ResourceType { get; }
    public long CreatedAt { get; }
    public long UpdatedAt { get; }
    public string Etag { get; }
    public long ContentLength { get; }
    public string ContentType { get; }
}

