namespace RuoYi.Common.Oss
{
    public interface IOssClient
    {
        Task<string> UploadAsync(Stream fileStream, string fileName, string? contentType = null);
        Task<Stream?> DownloadAsync(string objectName);
        Task<bool> DeleteAsync(string objectName);
        Task<string> GetUrlAsync(string objectName, int expiryMinutes = 60);
        Task<bool> ExistsAsync(string objectName);
    }
}