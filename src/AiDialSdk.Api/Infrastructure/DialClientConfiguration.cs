namespace AiDialSdk.Api.Infrastructure;

public class DialClientConfiguration
{
    public Uri? BaseUrl { get; set; }
    
    public string? ApiKey { get; set; }
    
    public Uri GetBaseUrlOrThrow()
    {
        return BaseUrl == null ? throw new ArgumentNullException(nameof(BaseUrl), "Dial API base URL is not set.") : BaseUrl;
    }
    
    public string GetApiKeyOrThrow()
    {
        return string.IsNullOrEmpty(ApiKey) ? throw new ArgumentNullException(nameof(ApiKey), "Dial API key is not set.") : ApiKey;
    }
}