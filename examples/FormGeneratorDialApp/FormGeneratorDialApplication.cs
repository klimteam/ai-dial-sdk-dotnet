using System.Text.Json;
using AiDialSdk.Api.Clients;
using AiDialSdk.Api.Data;
using AiDialSdk.Api.OpenAi.Data;
using AiDialSdk.Applications;
using FormGeneratorDialApp.Data;
using FormGeneratorDialApp.Helpers;

namespace FormGeneratorDialApp;

public class FormGeneratorDialApplication : IDialApplication
{
    private readonly IDialApiClient _dialApiClient;
    
    public FormGeneratorDialApplication(IDialApiClient dialApiClient)
    {
        _dialApiClient = dialApiClient;
    }
    
    public async Task CompleteChoicesAsync(DialChoiceCompletionContext completionContext, CancellationToken token)
    {
        var choice = completionContext.Choices.Single();
        
        const string deploymentName = "glm-5.3-flash";
        
        var client = _dialApiClient.GetChatClient(deploymentName, "2024-06-01-preview");
        
        var metaSchemaContent = await MetaSchemaHelper.ReadMetaSchemaAsync(token);
        var metaSchema = JsonElement.Parse(metaSchemaContent);
        var jsonSchema = new JsonSchema(metaSchema, "Description", "form_schema", true);
        
        var dialChatOptions = new DialChatOptions
        {
            ResponseFormat = new ResponseFormatJsonSchema(jsonSchema)
        };

        var requestMessages = completionContext.ChatCompletionRequest
            .Messages;
        
        // If the last message is a submitted form, echo its key/value pairs back as content.
        if (requestMessages.LastOrDefault() is DialUserMessage { CustomContent.FormValue: { } formValue })
        {
            var pairs = formValue.Select(kv => $"{kv.Key}: {kv.Value ?? "<empty>"}");
            await choice.AppendContentAsync(string.Join(Environment.NewLine, pairs), token);
            return;
        }
        
        var result = await client.CompleteChatAsync(
            requestMessages,
            dialChatOptions, 
            token);
        
        var formSchemaContent = result.Message.Content
                                ?? throw new InvalidOperationException("No content found in the chat completion result.");
        
        var llmFormSchema = JsonSerializer.Deserialize<LlmFormSchema>(formSchemaContent,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                            ?? throw new InvalidOperationException("Failed to deserialize the LLM form schema.");
        
        var dialFormSchema = FormSchemaConverter.Convert(llmFormSchema);
        
        await choice.AppendFormSchemaAsync(dialFormSchema, token);
    }

    public Task<DialFormSchema> GetConfigurationAsync(CancellationToken token)
    {
        return Task.FromResult(DialFormSchema.EmptyChatEnabled);
    }
}