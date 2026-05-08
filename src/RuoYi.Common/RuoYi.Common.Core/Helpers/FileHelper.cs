namespace RuoYi.Common.Core.Helpers
{
    public class FileHelper
    {
        public static string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName) ?? string.Empty;
        }

        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public static string GetDirectoryName(string filePath)
        {
            return Path.GetDirectoryName(filePath) ?? string.Empty;
        }

        public static string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static long GetFileSize(string filePath)
        {
            if (File.Exists(filePath))
            {
                return new FileInfo(filePath).Length;
            }
            return 0;
        }

        public static string GetFileSizeString(long size)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            int unitIndex = 0;
            double fileSize = size;

            while (fileSize >= 1024 && unitIndex < units.Length - 1)
            {
                fileSize /= 1024;
                unitIndex++;
            }

            return $"{fileSize:F2} {units[unitIndex]}";
        }

        public static string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public static byte[] ReadAllBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public static void WriteAllText(string filePath, string content)
        {
            var dir = GetDirectoryName(filePath);
            CreateDirectory(dir);
            File.WriteAllText(filePath, content);
        }

        public static void WriteAllBytes(string filePath, byte[] content)
        {
            var dir = GetDirectoryName(filePath);
            CreateDirectory(dir);
            File.WriteAllBytes(filePath, content);
        }
    }
}