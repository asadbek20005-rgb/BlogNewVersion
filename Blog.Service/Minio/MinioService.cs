using Blog.Service.Contracts;
using Microsoft.VisualBasic;
using Minio.DataModel.Args;
using Minio;
using System.Text.RegularExpressions;

namespace Blog.Service.Minio;

public class MinioService : IMinioService
{
    private readonly MinioClient _minioClient;
    private string minioUrl = "http://localhost:9000";
    private const string buckeName = "blog-bucket";
    public MinioService(string endpoint, string accessKey, string secretKey)
    {
        _minioClient = (MinioClient)new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();
    }


    public async Task<Stream> GetFileAsync(string objectName)
    {
        try
        {
            var ms = new MemoryStream();
            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(buckeName)
                .WithObject(objectName)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(ms);
                }));
            ms.Position = 0;
            return ms;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UploadFileAsync(string objectName, Stream data, long size, string contentType)
    {
        try
        {

            if (data.CanSeek)
            {
                data.Seek(0, SeekOrigin.Begin);
            }
            await EnsureBucketExistsAsync();
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(buckeName)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(size)
                .WithContentType(contentType));

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task EnsureBucketExistsAsync()
    {
        bool result = IsValidBucketName(buckeName);
        if (!result)
            throw new Exception("Bucket validation failed");

        var found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(buckeName));

        if (!found)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(buckeName));
        }

    }

    private bool IsValidBucketName(object bucketName)
    {
        throw new NotImplementedException();
    }

    public string GenerateUrl(string bucketName, string objectName)
    {
        return $"{minioUrl}/{bucketName}/{objectName}";
    }

    private static bool IsValidBucketName(string bucketName)
    {
        string pattern = @"^[a-z0-9][a-z0-9.-]{1,61}[a-z0-9]$";
        return Regex.IsMatch(bucketName, pattern);
    }
}
