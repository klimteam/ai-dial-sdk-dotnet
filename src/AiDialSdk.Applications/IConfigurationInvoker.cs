using Microsoft.AspNetCore.Http;

namespace AiDialSdk.Applications;

public interface IConfigurationInvoker
{
    Task InvokeAsync(string deploymentName, HttpRequest request, HttpResponse response, CancellationToken token);
}