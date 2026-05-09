using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("monitor/operlog")]
    public class SysOperLogController : BaseController
    {
        private readonly SysOperLogService _operLogService;
        public SysOperLogController(SysOperLogService operLogService) => _operLogService = operLogService;

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] Models.SysOperLog log)
        {
            var list = await _operLogService.SelectOperLogPageAsync(log);
            return Success(list);
        }

        [HttpDelete("{operIds}")]
        public async Task<AjaxResult> Remove(string operIds)
        {
            var ids = operIds.Split(',').Select(long.Parse).ToArray();
            await _operLogService.DeleteAsync(ids);
            return Success();
        }

        [HttpDelete("clean")]
        public async Task<AjaxResult> Clean()
        {
            await _operLogService.CleanAsync();
            return Success();
        }
    }
}