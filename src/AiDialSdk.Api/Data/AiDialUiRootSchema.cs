using System.Text.Json.Serialization;

namespace AiDialSdk.Api.Data;

public class AiDialWidgetOptions
{
    public AiDialWidgetOptions(string? confirmationMessage, string? populateText, bool? submit)
    {
        ConfirmationMessage = confirmationMessage;
        PopulateText = populateText;
        Submit = submit;
    }
    
    public string? ConfirmationMessage { get; }
    
    public string? PopulateText { get; }
    
    public bool? Submit { get; }
}

public class AiDialButtonOption
{
    public AiDialButtonOption(string title, int @const, AiDialWidgetOptions widgetOptions)
    {
        Title = title;
        Const = @const;
        WidgetOptions = widgetOptions;
    }
    
    public string Title { get; }
    
    public int Const { get; }
    
    [JsonPropertyName("dial:widgetOptions")]
    public AiDialWidgetOptions WidgetOptions { get; }
}

public enum AiDialWidgetType : byte
{
    Buttons
}

public enum SimpleType : byte
{
    Array,
    Boolean,
    Integer,
    Null,
    Number,
    Object,
    String
}

[JsonDerivedType(typeof(AiDialButton))]
public abstract class AiDialBaseControl
{
    protected AiDialBaseControl(string? description, SimpleType type)
    {
        Description = description;
        Type = type;
    }

    public string? Description { get; }
    
    public SimpleType Type { get; }
}

public class AiDialButton : AiDialBaseControl
{
    public AiDialButton(string? description, SimpleType type, AiDialWidgetType widgetType, IReadOnlyList<AiDialButtonOption> oneOf) 
        : base(description, type)
    {
        OneOf = oneOf;
        WidgetType = widgetType;
    }
    
    [JsonPropertyName("dial:widget")]
    public AiDialWidgetType WidgetType { get; }
    
    public IReadOnlyList<AiDialButtonOption> OneOf { get; }
}