using System.Text.Json.Serialization;

namespace AiDialSdk.Api.Data;

public class AiDialWidgetOptions
{
    public AiDialWidgetOptions(
        string? confirmationMessage = null, 
        string? populateText = null, 
        bool? submit = null,
        string? submitLabel = null)
    {
        ConfirmationMessage = confirmationMessage;
        PopulateText = populateText;
        Submit = submit;
        SubmitLabel = submitLabel;
    }
    
    public string? ConfirmationMessage { get; }
    
    public string? PopulateText { get; }
    
    public bool? Submit { get; }
    
    public string? SubmitLabel { get; }
}

public class AiDialButtonOption
{
    [JsonConstructor]
    public AiDialButtonOption(string title, object @const, AiDialWidgetOptions widgetOptions)
    {
        Title = title;
        Const = @const;
        WidgetOptions = widgetOptions;
    }

    [Obsolete("Use object const overload")]
    public AiDialButtonOption(string title, int @const, AiDialWidgetOptions widgetOptions)
        : this(title, (object)@const, widgetOptions) { }
    
    public string Title { get; }
    
    public object Const { get; }
    
    [JsonPropertyName("dial:widgetOptions")]
    public AiDialWidgetOptions WidgetOptions { get; }
}

public enum AiDialWidgetType : byte
{
    Buttons,
    Form
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

public interface IObjectSchema
{
    IReadOnlyDictionary<string, AiDialBaseControl> Properties { get; }
    IReadOnlyCollection<string> Required { get; }
}

[JsonConverter(typeof(JsonConverters.BaseControlJsonConverter))]
public abstract class AiDialBaseControl
{
    protected AiDialBaseControl(string? title, string? description, SimpleType type)
    {
        Title = title;
        Description = description;
        Type = type;
    }

    public string? Title { get; }
    
    public string? Description { get; }
    
    public SimpleType Type { get; }
}

public class AiDialButtons : AiDialBaseControl
{
    public AiDialButtons(string? title, string? description, SimpleType type, IReadOnlyList<AiDialButtonOption> oneOf) 
        : base(title, description, type)
    {
        OneOf = oneOf;
    }
    
    [JsonPropertyName("dial:widget")]
    public AiDialWidgetType WidgetType => AiDialWidgetType.Buttons;
    
    public IReadOnlyList<AiDialButtonOption> OneOf { get; }
}

public class AiDialCheckbox : AiDialBaseControl
{
    public AiDialCheckbox(string? title, string? description, bool @default) 
        : base(title, description, SimpleType.Boolean)
    {
        Default = @default;
    }
    
    [JsonPropertyName("default")]
    public bool Default { get; }
}

public class AiDialInputControl : AiDialBaseControl
{
    public AiDialInputControl(
        string? title, 
        string? description, 
        SimpleType type,
        object? @default = null,
        int? minLength = null,
        int? maxLength = null,
        double? minimum = null,
        double? maximum = null,
        string? pattern = null,
        string? format = null,
        IReadOnlyList<object>? @enum = null) 
        : base(title, description, type)
    {
        Default = @default;
        MinLength = minLength;
        MaxLength = maxLength;
        Minimum = minimum;
        Maximum = maximum;
        Pattern = pattern;
        Format = format;
        Enum = @enum;
    }

    [JsonPropertyName("default")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Default { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MinLength { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaxLength { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Minimum { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Maximum { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Pattern { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Format { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<object>? Enum { get; }
}

public class AiDialForm : AiDialBaseControl, IObjectSchema
{
    public AiDialForm(string? title, string? description, IReadOnlyDictionary<string, AiDialBaseControl> properties, IReadOnlyCollection<string>? required = null, AiDialWidgetOptions? widgetOptions = null)
        : base(title, description, SimpleType.Object)
    {
        Properties = properties;
        Required = required ?? Array.Empty<string>();
        WidgetOptions = widgetOptions ?? new AiDialWidgetOptions();
    }
    
    [JsonPropertyName("dial:widget")]
    public AiDialWidgetType WidgetType => AiDialWidgetType.Form;
    
    public IReadOnlyDictionary<string, AiDialBaseControl> Properties { get; }
    
    public IReadOnlyCollection<string> Required { get; }
    
    [JsonPropertyName("dial:widgetOptions")]
    public AiDialWidgetOptions WidgetOptions { get; }
}