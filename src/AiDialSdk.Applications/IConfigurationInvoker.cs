using Microsoft.AspNetCore.Http;

public interface IConfigurationInvoker
{
    Task InvokeAsync(string deploymentName, HttpRequest request, HttpResponse response, CancellationToken token);
}