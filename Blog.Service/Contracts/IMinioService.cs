namespace Blog.Service.Contracts;

public interface IMinioService
{
    public Task<Stream> GetFileAsync(string objectName);
    public Task UploadFileAsync(string objectName, Stream data, long size, string contentType);
    public Task EnsureBucketExistsAsync();
    public string GenerateUrl(string bucketName, string objectName);


}
