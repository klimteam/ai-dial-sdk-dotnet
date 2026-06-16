namespace AiDialSdk.Applications.Infrastructures;

public class DialApplicationConfiguration
{
    public Uri? Uri { get; set; }
    
    public Uri GetUriOrThrow()
    {
        if (Uri is null)
            throw new ArgumentException("Uri is not configured");
        return Uri;
    }
}