using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("system/config")]
    public class SysConfigController : BaseController
    {
        private readonly SysConfigService _configService;
        public SysConfigController(SysConfigService configService) => _configService = configService;

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] Models.SysConfig config)
        {
            var list = await _configService.SelectConfigPageAsync(config);
            return Success(list);
        }

        [HttpGet("{configId}")]
        public async Task<AjaxResult> Get(long configId)
        {
            var config = await _configService.GetByIdAsync(configId);
            return Success(config);
        }

        [HttpGet("key/{configKey}")]
        public async Task<AjaxResult> GetByKey(string configKey)
        {
            var config = await _configService.GetByKeyAsync(configKey);
            return Success(config);
        }

        [HttpPost]
        public async Task<AjaxResult> Add([FromBody] Models.SysConfig config)
        {
            await _configService.AddAsync(config);
            return Success();
        }

        [HttpPut]
        public async Task<AjaxResult> Edit([FromBody] Models.SysConfig config)
        {
            await _configService.UpdateAsync(config);
            return Success();
        }

        [HttpDelete("{configIds}")]
        public async Task<AjaxResult> Remove(string configIds)
        {
            var ids = configIds.Split(',').Select(long.Parse).ToArray();
            await _configService.DeleteAsync(ids);
            return Success();
        }
    }
}