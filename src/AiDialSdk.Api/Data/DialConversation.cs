using AiDialSdk.Api.OpenAi.Data;

namespace AiDialSdk.Api.Data;

public class DialConversation
{
    public DialConversation(
        string id, 
        string reference, 
        string name, 
        DialModel model, 
        string prompt, 
        double temperature, 
        string folderId, 
        List<BaseMessage> messages, 
        long updatedAt)
    {
        Id = id;
        Reference = reference;
        Name = name;
        Model = model;
        Prompt = prompt;
        Temperature = temperature;
        FolderId = folderId;
        Messages = messages;
        UpdatedAt = updatedAt;
    }

    public string Id { get; }
    public string Reference { get; }
    public string Name { get; }
    public DialModel Model { get; }
    public string Prompt { get; }
    public double Temperature { get; }
    public string FolderId { get; }
    public List<BaseMessage> Messages { get; }
    // ToDo: investigate format of this field.
    // public IReadOnlyList<string> SelectedAddons { get; set; }
    public long UpdatedAt { get; }
}

public class DialModel(string id)
{
    public string Id { get; } = id;
}