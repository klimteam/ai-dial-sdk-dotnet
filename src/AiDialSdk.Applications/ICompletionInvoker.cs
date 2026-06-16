using Microsoft.AspNetCore.Http;

namespace AiDialSdk.Applications;

public interface ICompletionInvoker
{
    Task InvokeAsync(string deploymentName, HttpRequest request, HttpResponse response, CancellationToken token);
}