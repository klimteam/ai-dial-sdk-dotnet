using AiDialSdk.Api.Data;
using AiDialSdk.Api.Infrastructure;
using AiDialSdk.Api.OpenAi.Data;
using Microsoft.AspNetCore.Http;

namespace AiDialSdk.Applications.Models;

public class ApplicationResponse : BaseApplicationResponse, IApplicationResponse
{
    private DialChatCompletion ChatCompletion { get; set; }
    
    public ApplicationResponse(string id, int created, string model, ChatCompletionRequest chatCompletionRequest, HttpResponse httpResponse) 
        : base(id, created, model, chatCompletionRequest, httpResponse)
    {
        ChatCompletion = new DialChatCompletion(id, created, model, null, SystemFingerprint, [], null);
    }
    
    public Task AppendDeltaChoiceAsync(DialDeltaChoice deltaChoice, CancellationToken _)
    {
        ChatCompletion = AppendDeltaChoiceToChatCompletion(ChatCompletion, deltaChoice);
        return Task.CompletedTask;
    }

    public async Task DoneAsync(CancellationToken token)
    {
        await HttpResponse.WriteAsJsonAsync(
            ChatCompletion, 
            GlobalJsonSettings.ChatCompletionResponseJsonSerializerOptions, 
            cancellationToken: token);
    }

    public Task AppendUsageAsync(Usage usage, CancellationToken _)
    {
        if (ChatCompletion.Usage is not null)
            throw new Exception("Usage can only be set once.");

        ChatCompletion = AppendUsageToChatCompletion(ChatCompletion, usage);
        return Task.CompletedTask;
    }
    
    private static DialChatCompletion AppendDeltaChoiceToChatCompletion(DialChatCompletion chatCompletion, DialDeltaChoice deltaChoice)
    {
        var choices = chatCompletion.Choices.ToList();
        
        if (deltaChoice.Index == chatCompletion.Choices.Count)
        {
            choices.Add(ConvertToChoice(deltaChoice));
        }
        else if (deltaChoice.Index < chatCompletion.Choices.Count)
        {
            choices[deltaChoice.Index] = choices[deltaChoice.Index].Combine(deltaChoice);
        }
        else
        {
            throw new Exception("Choice indexes must be sequential");
        }
        
        return new DialChatCompletion(
            chatCompletion.Id,
            chatCompletion.Created,
            chatCompletion.Model,
            chatCompletion.ServiceTier,
            chatCompletion.SystemFingerprint,
            choices,
            chatCompletion.Usage);
    }
    
    private static DialChatCompletion AppendUsageToChatCompletion(DialChatCompletion chatCompletion, Usage usage)
    {
        return new DialChatCompletion(
            chatCompletion.Id,
            chatCompletion.Created,
            chatCompletion.Model,
            chatCompletion.ServiceTier,
            chatCompletion.SystemFingerprint,
            chatCompletion.Choices,
            usage);
    }
    
    private static DialChoice ConvertToChoice(DialDeltaChoice deltaChoice)
    {
        return new DialChoice(
            deltaChoice.Index, 
            deltaChoice.Delta, 
            deltaChoice.LogProbability, 
            deltaChoice.FinishReason ?? FinishReason.Stop);
    }
}