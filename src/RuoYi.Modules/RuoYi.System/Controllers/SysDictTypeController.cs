using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("system/dict/type")]
    public class SysDictTypeController : BaseController
    {
        private readonly SysDictTypeService _dictTypeService;
        public SysDictTypeController(SysDictTypeService dictTypeService) => _dictTypeService = dictTypeService;

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] Models.SysDictType dict)
        {
            var list = await _dictTypeService.SelectDictTypePageAsync(dict);
            return Success(list);
        }

        [HttpGet("{dictId}")]
        public async Task<AjaxResult> Get(long dictId)
        {
            var dict = await _dictTypeService.GetByIdAsync(dictId);
            return Success(dict);
        }

        [HttpGet("all")]
        public async Task<AjaxResult> All()
        {
            var list = await _dictTypeService.GetListAsync();
            return Success(list);
        }

        [HttpPost]
        public async Task<AjaxResult> Add([FromBody] Models.SysDictType dict)
        {
            await _dictTypeService.AddAsync(dict);
            return Success();
        }

        [HttpPut]
        public async Task<AjaxResult> Edit([FromBody] Models.SysDictType dict)
        {
            await _dictTypeService.UpdateAsync(dict);
            return Success();
        }

        [HttpDelete("{dictIds}")]
        public async Task<AjaxResult> Remove(string dictIds)
        {
            var ids = dictIds.Split(',').Select(long.Parse).ToArray();
            await _dictTypeService.DeleteAsync(ids);
            return Success();
        }
    }
}