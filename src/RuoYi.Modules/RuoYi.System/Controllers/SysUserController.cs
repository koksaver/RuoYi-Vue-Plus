using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Models;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("system/user")]
    public class SysUserController : BaseController
    {
        private readonly SysUserService _userService;

        public SysUserController(SysUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] SysUser user)
        {
            var list = await _userService.GetPageAsync(user, user.PageNum, user.PageSize);
            return Success(list);
        }

        [HttpGet("{userId}")]
        public async Task<AjaxResult> Get(long userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            return Success(user);
        }

        [HttpPost]
        public async Task<AjaxResult> Add([FromBody] SysUser user)
        {
            await _userService.AddAsync(user);
            return Success();
        }

        [HttpPut]
        public async Task<AjaxResult> Edit([FromBody] SysUser user)
        {
            await _userService.UpdateAsync(user);
            return Success();
        }

        [HttpDelete("{userIds}")]
        public async Task<AjaxResult> Remove(string userIds)
        {
            var ids = userIds.Split(',').Select(long.Parse).ToArray();
            await _userService.DeleteAsync(ids);
            return Success();
        }

        [HttpPut("resetPwd")]
        public async Task<AjaxResult> ResetPwd([FromBody] SysUser user)
        {
            await _userService.ResetPasswordAsync(user.UserId, UserConstants.DEFAULT_PASSWORD);
            return Success();
        }

        [HttpPut("changeStatus")]
        public async Task<AjaxResult> ChangeStatus([FromBody] SysUser user)
        {
            await _userService.UpdateStatusAsync(user.UserId, user.Status ?? "0");
            return Success();
        }
    }
}