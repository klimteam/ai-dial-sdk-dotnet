using System.Text.Json.Serialization;

namespace AiDialSdk.Api.Data;

public class BucketResponse
{
    public BucketResponse(string bucket, string appData)
    {
        Bucket = bucket;
        AppData = appData;
    }
    
    public string Bucket { get; }
    
    [JsonPropertyName("appdata")]
    public string AppData { get; }
}