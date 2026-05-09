using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("monitor/logininfor")]
    public class SysLoginInfoController : BaseController
    {
        private readonly SysLoginInfoService _loginInfoService;
        public SysLoginInfoController(SysLoginInfoService loginInfoService) => _loginInfoService = loginInfoService;

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] Models.SysLoginInfo info)
        {
            var list = await _loginInfoService.SelectLoginInfoPageAsync(info);
            return Success(list);
        }

        [HttpDelete("{infoIds}")]
        public async Task<AjaxResult> Remove(string infoIds)
        {
            var ids = infoIds.Split(',').Select(long.Parse).ToArray();
            await _loginInfoService.DeleteAsync(ids);
            return Success();
        }

        [HttpDelete("clean")]
        public async Task<AjaxResult> Clean()
        {
            await _loginInfoService.CleanAsync();
            return Success();
        }
    }
}