using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("system/dept")]
    public class SysDeptController : BaseController
    {
        private readonly SysDeptService _deptService;
        public SysDeptController(SysDeptService deptService) => _deptService = deptService;

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] Models.SysDept dept)
        {
            var list = await _deptService.SelectDeptListAsync(dept);
            return Success(list);
        }

        [HttpGet("treeselect")]
        public async Task<AjaxResult> TreeSelect()
        {
            var depts = await _deptService.SelectDeptListAsync(new Models.SysDept());
            var tree = _deptService.BuildDeptTree(depts);
            return Success(tree);
        }

        [HttpGet("{deptId}")]
        public async Task<AjaxResult> Get(long deptId)
        {
            var dept = await _deptService.GetByIdAsync(deptId);
            return Success(dept);
        }

        [HttpPost]
        public async Task<AjaxResult> Add([FromBody] Models.SysDept dept)
        {
            await _deptService.AddAsync(dept);
            return Success();
        }

        [HttpPut]
        public async Task<AjaxResult> Edit([FromBody] Models.SysDept dept)
        {
            await _deptService.UpdateAsync(dept);
            return Success();
        }

        [HttpDelete("{deptId}")]
        public async Task<AjaxResult> Remove(long deptId)
        {
            await _deptService.DeleteAsync(deptId);
            return Success();
        }
    }
}