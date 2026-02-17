namespace AiDialSdk.Api.Infrastructure;

public class DialClientConfiguration
{
    public Uri? Uri { get; set; }
    
    public string? ApiKey { get; set; }
    
    public Uri GetUriOrThrow()
    {
        return Uri == null ? throw new ArgumentNullException(nameof(Uri), "Dial API URI is not set.") : Uri;
    }
    
    public string GetApiKeyOrThrow()
    {
        return string.IsNullOrEmpty(ApiKey) ? throw new ArgumentNullException(nameof(ApiKey), "Dial API key is not set.") : ApiKey;
    }
}