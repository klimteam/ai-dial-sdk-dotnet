using Microsoft.AspNetCore.Http;

public interface ICompletionInvoker
{
    Task InvokeAsync(string deploymentName, HttpRequest request, HttpResponse response, CancellationToken token);
}