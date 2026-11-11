using AiDialSdk.Api.Data;
using AiDialSdk.Applications;

namespace FormDialApp;

public class FormDialApplication : IDialApplication
{
    private readonly Dictionary<string, AiDialBaseControl> _formControls = new()
    {
        ["firstName"] = new AiDialInputControl("First name", null, SimpleType.String, @default: "Chuck"),
        ["lastName"] = new AiDialInputControl("Last name", null, SimpleType.String),
        ["age"] = new AiDialInputControl("Age", null, SimpleType.Integer),
        ["bio"] = new AiDialInputControl("Bio", null, SimpleType.String),
        ["password"] = new AiDialInputControl("Password", null, SimpleType.String, minLength: 3),
        ["telephone"] = new AiDialInputControl("Telephone", null, SimpleType.String, minLength: 10),
    };
    
    public async Task CompleteChoicesAsync(DialChoiceCompletionContext completionContext, CancellationToken token)
    {
        var choice = completionContext.Choices.Single();
        
        var options = new AiDialWidgetOptions(submit: true, submitLabel: "Register");
        
        var form = new AiDialForm(
            "A registration form",
            "A simple form example.",
            _formControls,
            ["firstName", "lastName"],
            options);
        
         var controls = new Dictionary<string, AiDialBaseControl>
         {
             ["registrationForm"] = form
         };
        
         var dialFormSchema = new DialFormSchema(true, controls);
         
        await choice.AppendFormSchemaAsync(dialFormSchema, token);
    }

    public Task<DialFormSchema> GetConfigurationAsync(CancellationToken token)
    {
        return Task.FromResult(DialFormSchema.EmptyChatEnabled);
    }
}