using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Models;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("system/role")]
    public class SysRoleController : BaseController
    {
        private readonly SysRoleService _roleService;
        public SysRoleController(SysRoleService roleService) => _roleService = roleService;

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] SysRole role)
        {
            var list = await _roleService.GetPageAsync(role, role.PageNum, role.PageSize);
            return Success(list);
        }

        [HttpGet("{roleId}")]
        public async Task<AjaxResult> Get(long roleId)
        {
            var role = await _roleService.GetByIdAsync(roleId);
            return Success(role);
        }

        [HttpPost]
        public async Task<AjaxResult> Add([FromBody] SysRole role)
        {
            await _roleService.AddAsync(role);
            return Success();
        }

        [HttpPut]
        public async Task<AjaxResult> Edit([FromBody] SysRole role)
        {
            await _roleService.UpdateAsync(role);
            return Success();
        }

        [HttpDelete("{roleIds}")]
        public async Task<AjaxResult> Remove(string roleIds)
        {
            var ids = roleIds.Split(',').Select(long.Parse).ToArray();
            await _roleService.DeleteAsync(ids);
            return Success();
        }

        [HttpPut("authDataScope")]
        public async Task<AjaxResult> AuthDataScope([FromBody] SysRole role)
        {
            await _roleService.UpdateAsync(role);
            return Success();
        }
    }
}