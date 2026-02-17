using System.Text;
using System.Text.Json;
using AiDialSdk.Api.Clients.Implementations;
using AiDialSdk.Api.Data;
using AiDialSdk.Api.Infrastructure;

namespace AiDialSdk.Api.Files.Implementations;

public class DialFileApiClient : BaseApiClient, IDialFileApiClient
{
    public DialFileApiClient(HttpClient httpClient, Uri endpoint, string? apiKey) : base(httpClient, endpoint, apiKey)
    {
    }
    
    public async Task<PutFileResponse> StoreDataAsync(string fileName, Stream stream, CancellationToken token)
    {
        using var streamContent = new StreamContent(stream);
        return await StoreContentAsync(fileName, streamContent, token);
    }
    
    public async Task<PutFileResponse> StoreDataAsync(string fileName, string fileData, CancellationToken token)
    {
        using var stringContent = new StringContent(fileData, Encoding.UTF8, "text/plain");

        return await StoreContentAsync(fileName, stringContent, token);
    }

    private async Task<PutFileResponse> StoreContentAsync(string fileName, HttpContent httpContent, CancellationToken token)
    {
        var bucket = await GetBucketAsync(token);
        
        var storeFileUrl = BuildBucketJsonFileUri(Endpoint, bucket.AppData, fileName);
        
        using var form = BuildFileUploadFormContent(httpContent, fileName);
        
        var request = new HttpRequestMessage(HttpMethod.Put, storeFileUrl)
        {
            Content = form
        };
        
        var response = await SendAsync(request, token);
        
        var content = await response.Content.ReadAsStringAsync(token);
        var putFileResponse = JsonSerializer.Deserialize<PutFileResponse>(content, GlobalJsonSettings.DefaultJsonSerializerOptions);

        return putFileResponse ?? throw new Exception("File put response is null");
    }
    
    private async Task<BucketResponse> GetBucketAsync(CancellationToken token)
    {
        var bucketUrl = BuildBucketUri(Endpoint);
        var request = new HttpRequestMessage(HttpMethod.Get, bucketUrl);

        var response = await SendAsync(request, token);

        var content = await response.Content.ReadAsStringAsync(token);
        var bucketResponse = JsonSerializer.Deserialize<BucketResponse>(content, GlobalJsonSettings.DefaultJsonSerializerOptions);
        
        return bucketResponse ?? throw new Exception("Bucket response is null");
    }

    private static Uri BuildBucketUri(Uri endpoint)
    {
        return new Uri($"{endpoint}/v1/bucket");
    }
    
    private static Uri BuildBucketJsonFileUri(Uri endpoint, string appData, string fileName)
    {
        return new Uri($"{endpoint}/v1/files/{appData}/{fileName}");
    }
    
    private static MultipartFormDataContent BuildFileUploadFormContent(HttpContent content, string fileName)
    {
        var form = new MultipartFormDataContent();
        form.Add(content, "file", fileName);
        return form;
    }
}