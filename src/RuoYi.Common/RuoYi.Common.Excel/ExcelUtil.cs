using MiniExcelLibs;
using RuoYi.Common.Core;

namespace RuoYi.Common.Excel
{
    public class ExcelUtil
    {
        public static byte[] ExportToBytes<T>(List<T> data, string sheetName = "Sheet1")
        {
            using var stream = new MemoryStream();
            stream.SaveAs(sheetName, data);
            return stream.ToArray();
        }

        public static void ExportToFile<T>(List<T> data, string filePath, string sheetName = "Sheet1")
        {
            using var stream = File.Create(filePath);
            stream.SaveAs(sheetName, data);
        }

        public static List<T> ImportFromFile<T>(string filePath) where T : class, new()
        {
            using var stream = File.OpenRead(filePath);
            return stream.Query<T>().ToList();
        }

        public static List<T> ImportFromBytes<T>(byte[] bytes) where T : class, new()
        {
            using var stream = new MemoryStream(bytes);
            return stream.Query<T>().ToList();
        }
    }
}