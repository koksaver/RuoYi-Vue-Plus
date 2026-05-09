using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("system/menu")]
    public class SysMenuController : BaseController
    {
        private readonly SysMenuService _menuService;
        public SysMenuController(SysMenuService menuService) => _menuService = menuService;

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] Models.SysMenu menu)
        {
            var list = await _menuService.SelectMenuListAsync(menu);
            return Success(list);
        }

        [HttpGet("treeselect")]
        public async Task<AjaxResult> TreeSelect()
        {
            var menus = await _menuService.SelectMenuTreeListAsync();
            var tree = _menuService.BuildMenuTree(menus);
            return Success(tree);
        }

        [HttpGet("{menuId}")]
        public async Task<AjaxResult> Get(long menuId)
        {
            var menu = await _menuService.GetByIdAsync(menuId);
            return Success(menu);
        }

        [HttpPost]
        public async Task<AjaxResult> Add([FromBody] Models.SysMenu menu)
        {
            await _menuService.AddAsync(menu);
            return Success();
        }

        [HttpPut]
        public async Task<AjaxResult> Edit([FromBody] Models.SysMenu menu)
        {
            await _menuService.UpdateAsync(menu);
            return Success();
        }

        [HttpDelete("{menuId}")]
        public async Task<AjaxResult> Remove(long menuId)
        {
            await _menuService.DeleteAsync(menuId);
            return Success();
        }
    }
}