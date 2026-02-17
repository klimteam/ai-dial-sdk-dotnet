namespace AiDialSdk.Api.Data;

public class DialRequestCustomFields
{
    public DialRequestCustomFields(DialConfigurationValues? configuration)
    {
        Configuration = configuration;
    }
    
    public DialConfigurationValues? Configuration { get; }
}