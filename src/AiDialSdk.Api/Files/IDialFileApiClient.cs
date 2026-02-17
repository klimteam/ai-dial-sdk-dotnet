using AiDialSdk.Api.Data;

namespace AiDialSdk.Api.Files;

public interface IDialFileApiClient
{
    Task<PutFileResponse> StoreDataAsync(string fileName, Stream stream, CancellationToken token);
    Task<PutFileResponse> StoreDataAsync(string fileName, string fileData, CancellationToken token);
}