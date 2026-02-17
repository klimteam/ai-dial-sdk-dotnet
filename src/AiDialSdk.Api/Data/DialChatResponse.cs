using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialChatResponse
{
    public DialChatResponse(string id, int created, string model, Usage? usage, DialAssistantMessage message)
    {
        Id = id;
        Created = created;
        Model = model;
        Usage = usage;
        Message = message;
    }
    
    
    public string Id { get; }

    public int Created { get; }
    
    public string Model { get; }
    
    public Usage? Usage { get; }
    
    public DialAssistantMessage Message { get; }
}