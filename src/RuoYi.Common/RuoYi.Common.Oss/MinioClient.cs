using Minio;
using Minio.DataModel.Args;

namespace RuoYi.Common.Oss
{
    public class MinioClient : IOssClient
    {
        private readonly IMinioClient _minioClient;
        private readonly string _bucketName;

        public MinioClient(string endpoint, string accessKey, string secretKey, string bucketName, bool useSsl = false)
        {
            _minioClient = new Minio.MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .WithSSL(useSsl)
                .Build();
            _bucketName = bucketName;
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string? contentType = null)
        {
            var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
            if (!bucketExists)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType ?? "application/octet-stream");

            await _minioClient.PutObjectAsync(putObjectArgs);

            return $"{_bucketName}/{fileName}";
        }

        public async Task<Stream?> DownloadAsync(string objectName)
        {
            try
            {
                var stream = new MemoryStream();
                var getObjectArgs = new GetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithCallbackStream(s => s.CopyTo(stream));

                await _minioClient.GetObjectAsync(getObjectArgs);
                stream.Position = 0;
                return stream;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(string objectName)
        {
            try
            {
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName);

                await _minioClient.RemoveObjectAsync(removeObjectArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetUrlAsync(string objectName, int expiryMinutes = 60)
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithExpiry(expiryMinutes);

            return await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
        }

        public async Task<bool> ExistsAsync(string objectName)
        {
            try
            {
                var statObjectArgs = new StatObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName);

                await _minioClient.StatObjectAsync(statObjectArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}