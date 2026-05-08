using Microsoft.Extensions.Logging;
using RuoYi.Common.Core;

namespace RuoYi.Common.Excel
{
    public class ExcelImportService
    {
        private readonly ILogger<ExcelImportService> _logger;

        public ExcelImportService(ILogger<ExcelImportService> logger)
        {
            _logger = logger;
        }

        public async Task<List<T>> ImportAsync<T>(string filePath) where T : class, new()
        {
            try
            {
                var data = ExcelUtil.ImportFromFile<T>(filePath);
                _logger.LogInformation("成功导入 {Count} 条记录", data.Count);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel导入失败: {Path}", filePath);
                throw;
            }
        }

        public async Task<List<T>> ImportAsync<T>(byte[] bytes) where T : class, new()
        {
            try
            {
                var data = ExcelUtil.ImportFromBytes<T>(bytes);
                _logger.LogInformation("成功导入 {Count} 条记录", data.Count);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel导入失败");
                throw;
            }
        }
    }
}