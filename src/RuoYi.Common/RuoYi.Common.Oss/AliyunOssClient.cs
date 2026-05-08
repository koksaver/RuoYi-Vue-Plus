using Aliyun.OSS;

namespace RuoYi.Common.Oss
{
    public class AliyunOssClient : IOssClient
    {
        private readonly OssClient _ossClient;
        private readonly string _bucketName;

        public AliyunOssClient(string endpoint, string accessKeyId, string accessKeySecret, string bucketName)
        {
            _ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
            _bucketName = bucketName;
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string? contentType = null)
        {
            var objectMeta = new ObjectMetadata();
            if (!string.IsNullOrEmpty(contentType))
            {
                objectMeta.ContentType = contentType;
            }

            var result = await Task.Run(() =>
                _ossClient.PutObject(_bucketName, fileName, fileStream, objectMeta));

            return $"{_bucketName}/{fileName}";
        }

        public async Task<Stream?> DownloadAsync(string objectName)
        {
            try
            {
                var result = await Task.Run(() => _ossClient.GetObject(_bucketName, objectName));
                var stream = new MemoryStream();
                result.Content.CopyTo(stream);
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
                var result = await Task.Run(() => _ossClient.DeleteObject(_bucketName, objectName));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetUrlAsync(string objectName, int expiryMinutes = 60)
        {
            var uri = await Task.Run(() =>
                _ossClient.GeneratePresignedUri(_bucketName, objectName,
                    DateTime.UtcNow.AddMinutes(expiryMinutes)));

            return uri.ToString();
        }

        public async Task<bool> ExistsAsync(string objectName)
        {
            try
            {
                var result = await Task.Run(() => _ossClient.DoesObjectExist(_bucketName, objectName));
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}