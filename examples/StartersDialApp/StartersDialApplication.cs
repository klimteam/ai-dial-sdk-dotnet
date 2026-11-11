using AiDialSdk.Api.Data;
using AiDialSdk.Applications;

namespace StartersDialApp;

public class StartersDialApplication : IDialApplication
{
    private readonly Dictionary<string, AiDialBaseControl> _buttons = new()
    {
        {
            "button", 
            new AiDialButtons(
                "Button Title Inner",
                "What do you want to do? Inner", 
                SimpleType.Number,
                [
                    new AiDialButtonOption("First", 0, new AiDialWidgetOptions(null, "Generate test form. With enabled input.", false)),
                    new AiDialButtonOption("Second", 1, new AiDialWidgetOptions(null, "Generate test form. With disabled input.", false))])
        },
        {
            "deep_research",
            new AiDialCheckbox("Deep research", "Do you want to enable deep research?", @default: false)
        }
    };
    
    public Task CompleteChoicesAsync(DialChoiceCompletionContext completionContext, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    public Task<DialFormSchema> GetConfigurationAsync(CancellationToken token)
    {
        var dialFormSchema = new DialFormSchema(false, _buttons);
        return Task.FromResult(dialFormSchema);
    }
}