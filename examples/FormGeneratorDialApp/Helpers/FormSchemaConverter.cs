using System.Text.Json;
using AiDialSdk.Api.Data;
using FormGeneratorDialApp.Data;

namespace FormGeneratorDialApp.Helpers;

public static class FormSchemaConverter
{
    public static DialFormSchema Convert(LlmFormSchema llmSchema, string formKey = "form")
    {
        var properties = new Dictionary<string, AiDialBaseControl>();
        var required = new List<string>();

        foreach (var field in llmSchema.Fields)
        {
            properties[field.Name] = ConvertField(field);
            if (field.Required)
                required.Add(field.Name);
        }

        var form = new AiDialForm(
            llmSchema.Title,
            llmSchema.Description,
            properties,
            required,
            new AiDialWidgetOptions(submit: true));

        return new DialFormSchema(
            llmSchema.ChatMessageInputDisabled,
            new Dictionary<string, AiDialBaseControl> { [formKey] = form });
    }

    private static AiDialBaseControl ConvertField(LlmFormField field)
    {
        var type = field.Type?.ToLowerInvariant() switch
        {
            "string" => SimpleType.String,
            "number" => SimpleType.Number,
            "integer" => SimpleType.Integer,
            "boolean" => SimpleType.Boolean,
            _ => SimpleType.String
        };

        return new AiDialInputControl(
            field.Title,
            field.Description,
            type,
            @default: ExtractValue(field.Default),
            minLength: field.MinLength,
            maxLength: field.MaxLength,
            minimum: field.Minimum,
            maximum: field.Maximum,
            pattern: field.Pattern,
            format: field.Format,
            @enum: field.Enum?.Select(e => ExtractValue(e)).ToList());
    }

    private static object? ExtractValue(JsonElement? element)
    {
        if (element is not { } e)
            return null;

        return e.ValueKind switch
        {
            JsonValueKind.String => e.GetString(),
            JsonValueKind.Number => e.TryGetInt64(out var l) ? l : e.GetDouble(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null,
            _ => e.GetRawText()
        };
    }
}
