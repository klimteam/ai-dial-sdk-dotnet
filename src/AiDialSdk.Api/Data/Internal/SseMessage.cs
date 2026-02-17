namespace AiDialSdk.Api.Data.Internal;

internal record SseMessage
{
    public SseMessage(string? id, string? data, string? @event)
    {
        Id = id;
        Data = data;
        Event = @event;
    }
    
    public string? Id { get; }
    public string? Data { get; }
    public string? Event { get; }
}