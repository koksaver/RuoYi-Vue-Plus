using Microsoft.Extensions.Logging;
using RuoYi.Common.Core;

namespace RuoYi.Common.Log
{
    public class OperLogHandler
    {
        private readonly ILogger<OperLogHandler> _logger;

        public OperLogHandler(ILogger<OperLogHandler> logger)
        {
            _logger = logger;
        }

        public void RecordLog(OperLogInfo logInfo)
        {
            _logger.LogInformation(
                "操作日志 - 模块: {Module}, 业务类型: {BusinessType}, 操作人: {Operator}, IP: {Ip}, 状态: {Status}, 耗时: {CostTime}ms",
                logInfo.Title,
                logInfo.BusinessType,
                logInfo.OperName,
                logInfo.OperIp,
                logInfo.Status == 0 ? "成功" : "失败",
                logInfo.CostTime
            );
        }
    }

    public class OperLogInfo
    {
        public string? Title { get; set; }
        public int BusinessType { get; set; }
        public string? OperName { get; set; }
        public string? OperIp { get; set; }
        public string? OperLocation { get; set; }
        public string? RequestUrl { get; set; }
        public string? RequestMethod { get; set; }
        public string? Method { get; set; }
        public string? RequestParam { get; set; }
        public string? JsonResult { get; set; }
        public int Status { get; set; }
        public string? ErrorMsg { get; set; }
        public long CostTime { get; set; }
        public DateTime? OperTime { get; set; }
    }
}