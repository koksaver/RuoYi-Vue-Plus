using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("system/dict/data")]
    public class SysDictDataController : BaseController
    {
        private readonly SysDictDataService _dictDataService;
        public SysDictDataController(SysDictDataService dictDataService) => _dictDataService = dictDataService;

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] Models.SysDictData dict)
        {
            var list = await _dictDataService.SelectDictDataPageAsync(dict);
            return Success(list);
        }

        [HttpGet("{dictCode}")]
        public async Task<AjaxResult> Get(long dictCode)
        {
            var dict = await _dictDataService.GetByIdAsync(dictCode);
            return Success(dict);
        }

        [HttpGet("type/{dictType}")]
        public async Task<AjaxResult> GetByType(string dictType)
        {
            var list = await _dictDataService.GetDictDataByTypeAsync(dictType);
            return Success(list);
        }

        [HttpPost]
        public async Task<AjaxResult> Add([FromBody] Models.SysDictData dict)
        {
            await _dictDataService.AddAsync(dict);
            return Success();
        }

        [HttpPut]
        public async Task<AjaxResult> Edit([FromBody] Models.SysDictData dict)
        {
            await _dictDataService.UpdateAsync(dict);
            return Success();
        }

        [HttpDelete("{dictCodes}")]
        public async Task<AjaxResult> Remove(string dictCodes)
        {
            var ids = dictCodes.Split(',').Select(long.Parse).ToArray();
            await _dictDataService.DeleteAsync(ids);
            return Success();
        }
    }
}