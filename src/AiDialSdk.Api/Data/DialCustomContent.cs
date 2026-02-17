using System.Text.Json.Serialization;
using AiDialSdk.Api.Extensions;
using AiDialSdk.Api.JsonConverters;

namespace AiDialSdk.Api.Data;

public class DialCustomContent
{
    [JsonConstructor]
    public DialCustomContent(
        IReadOnlyList<DialAttachment>? attachments, 
        IReadOnlyList<DialStage>? stages, 
        DialFormSchema? formSchema, 
        DialState? state)
    {
        Attachments = attachments;
        Stages = stages;
        FormSchema = formSchema;
        State = state;
    }
    
    public DialCustomContent(DialAttachment attachment)
    {
        Attachments = [attachment];
        FormSchema = null;
        Stages = null;
        State = null;
    }
    
    public DialCustomContent(DialStage stage)
    {
        Attachments = null;
        FormSchema = null;
        Stages = [stage];
        State = null;
    }
    
    public DialCustomContent(DialFormSchema formSchema)
    {
        Attachments = null;
        Stages = null;
        FormSchema = formSchema;
        State = null;
    }

    public DialCustomContent(DialState state)
    {
        Attachments = null;
        Stages = null;
        FormSchema = null;
        State = state;
    }
    
    public IReadOnlyList<DialAttachment>? Attachments { get; init; }
    
    public IReadOnlyList<DialStage>? Stages { get; init; }

    [JsonConverter(typeof(DefaultJsonConverter<DialFormSchema>))]
    public DialFormSchema? FormSchema { get; init; }
    
    public DialState? State { get; init; }
    
    public DialCustomContent Combine(DialCustomContent other)
    {
        var attachments = Attachments.Combine(other.Attachments);
        var stages = Stages.Combine(other.Stages);

        var formSchema = FormSchema;
        if (formSchema is null)
        {
            formSchema = other.FormSchema;
        }
        else if (other.FormSchema is not null)
        {
            formSchema = formSchema.Combine(other.FormSchema);
        }

        var dialState = State;
        if (dialState is null)
        {
            dialState = other.State;
        }
        else if (other.State is not null)
        {
            dialState = dialState.Combine(other.State);
        }
        
        return new DialCustomContent(attachments, stages, formSchema, dialState);
    }
}