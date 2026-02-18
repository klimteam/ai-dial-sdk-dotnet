using System.Text;
using System.Text.Json;
using AiDialSdk.Api.Data;

namespace Models;

public class DialApplicationStage : IAsyncDisposable, IDisposable
{
    private bool _disposed;
    
    private bool _opened;
    private bool _closed;
    
    private readonly List<DialAttachment> _attachments;

    private StringBuilder? _name;
    private StringBuilder? _content;
    
    public int Index { get; }
    
    public string? Name => _name?.ToString();

    public string? Content => _content?.ToString();
    
    private DialApplicationChoice Choice { get; }
    
    internal DialApplicationStage(int index, DialApplicationChoice choice)
    {
        Index = index;
        Choice = choice;

        _attachments = [];
    }

    internal DialApplicationStage(int index, string? name, DialApplicationChoice choice)
    {
        Index = index;
        _name = new StringBuilder(name);
        Choice = choice;
        
        _attachments = [];
    }

    public async Task AppendContentLineAsync(CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot append.");
        
        if (!_opened)
            await OpenAsync(token);
        
        var dialStage = new DialStage(Index, null, null, "\n\n", null);
        await Choice.AppendStageDataAsync(dialStage, token);
    }
    
    public async Task AppendContentAsync(string content, CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot append.");
        
        if (!_opened)
            await OpenAsync(token);

        _content ??= new StringBuilder();
        _content.Append(content);
        
        var dialStage = new DialStage(Index, null, null, content, null);
        await Choice.AppendStageDataAsync(dialStage, token);
    }
    
    public async Task AppendLineContentAsync(CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot append.");
        
        if (!_opened)
            await OpenAsync(token);
        
        var dialStage = new DialStage(Index, null, null, "\n\n", null);
        await Choice.AppendStageDataAsync(dialStage, token);
    }
    
    public async Task AppendLineAndContentAsync(string content, CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot append.");

        if (!_opened)
            await OpenAsync(token);
        
        var lineAndContent = "\n\n" + content;
        
        _content ??= new StringBuilder();
        _content.AppendLine(content);
        
        var dialStage = new DialStage(Index, null, null, lineAndContent, null);
        await Choice.AppendStageDataAsync(dialStage, token);
    }

    public async Task AppendNameAsync(string name, CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot append.");
        
        if (!_opened)
            await OpenAsync(token);
        
        _name ??= new StringBuilder();
        _name.Append(name);
        
        var dialStage = new DialStage(Index, name, null, null, null);
        await Choice.AppendStageDataAsync(dialStage, token);
    }

    public async Task AppendLineNameAsync(CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot append.");
        
        if (!_opened)
            await OpenAsync(token);
        
        var line = "\n\n";
        
        _name ??= new StringBuilder();
        _name.Append(line);
        
        var dialStage = new DialStage(Index, line, null, null, null);
        await Choice.AppendStageDataAsync(dialStage, token);
    }
    
    public async Task AppendLineAndNameAsync(string name, CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot append.");
        
        if (!_opened)
            await OpenAsync(token);
        
        var lineAndName = "\n\n" + name;
        
        _name ??= new StringBuilder();
        _name.AppendLine();
        _name.Append(name);
        
        var dialStage = new DialStage(Index, lineAndName, null, null, null);
        await Choice.AppendStageDataAsync(dialStage, token);
    }
    
    public async Task AppendTextAttachmentAsync(string title, string data, CancellationToken token)
    {
        await AppendAttachmentDataAsync("text/plain", title, data, token);
    }
    
    public async Task AppendTextAttachmentAsync<TData>(string title, TData data, CancellationToken token)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        await AppendAttachmentDataAsync("text/plain", title, json, token);
    }
    
    public async Task AppendAttachmentDataAsync(string type, string title, string data, CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot add attachment to a closed choice.");
        
        if (!_opened)
            await OpenAsync(token);
        
        var attachment = new DialAttachment(_attachments.Count, type, title, data, null, null, null);

        _attachments.Add(attachment);
        
        await AppendAttachmentDataAsync(attachment, token);
    }
    
    public async Task AppendAttachmentUrlAsync(string type, string title, string url, CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot add attachment to a closed choice.");
        
        if (!_opened)
            await OpenAsync(token);
        
        var attachment = new DialAttachment(_attachments.Count, type, title, null, url, null, null);

        _attachments.Add(attachment);
        
        await AppendAttachmentDataAsync(attachment, token);
    }
    
    public async ValueTask OpenAsync(CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot open a closed stage.");
        
        if (_opened)
            return;
        
        var dialStage = new DialStage(Index, Name, null, null, null);
        await Choice.AppendStageDataAsync(dialStage, token);
        
        _opened = true;
    }

    public async Task CloseAsync(CancellationToken token)
    {
        await CloseAsync(DialStageStatus.Completed, token);
    }

    public async Task CloseAsync(Exception ex, CancellationToken token)
    {
        await AppendContentAsync(ex.Message, token);
        await CloseAsync(DialStageStatus.Failed, token);
    }
    
    public async Task CloseAsync(string reason, CancellationToken token)
    {
        await AppendContentAsync(reason, token);
        await CloseAsync(DialStageStatus.Failed, token);
    }
    
    private async Task AppendAttachmentDataAsync(DialAttachment dialAttachment, CancellationToken token)
    {
        if (_closed)
            throw new Exception("Cannot append attachment to a closed choice.");
        
        var dialStage = new DialStage(Index, null, null, null, [dialAttachment]);
        await Choice.AppendStageDataAsync(dialStage, token);
    }
    
    private async Task CloseAsync(DialStageStatus status, CancellationToken token)
    {
        if (_closed)
            return;

        if (!_opened)
            await OpenAsync(token);
        
        var dialStage = new DialStage(Index, null, status, null, null);
        await Choice.AppendStageDataAsync(dialStage, token);
        
        _closed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
            return;
        
        await CloseAsync(DialStageStatus.Completed, CancellationToken.None);

        _disposed = true;
        
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        DisposeAsync().AsTask().Wait();
    }
}