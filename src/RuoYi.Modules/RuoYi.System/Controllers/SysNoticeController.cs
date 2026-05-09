using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;
using RuoYi.System.Services;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("system/notice")]
    public class SysNoticeController : BaseController
    {
        private readonly SysNoticeService _noticeService;
        public SysNoticeController(SysNoticeService noticeService) => _noticeService = noticeService;

        [HttpGet("list")]
        public async Task<AjaxResult> List([FromQuery] Models.SysNotice notice)
        {
            var list = await _noticeService.SelectNoticePageAsync(notice);
            return Success(list);
        }

        [HttpGet("{noticeId}")]
        public async Task<AjaxResult> Get(long noticeId)
        {
            var notice = await _noticeService.GetByIdAsync(noticeId);
            return Success(notice);
        }

        [HttpPost]
        public async Task<AjaxResult> Add([FromBody] Models.SysNotice notice)
        {
            await _noticeService.AddAsync(notice);
            return Success();
        }

        [HttpPut]
        public async Task<AjaxResult> Edit([FromBody] Models.SysNotice notice)
        {
            await _noticeService.UpdateAsync(notice);
            return Success();
        }

        [HttpDelete("{noticeIds}")]
        public async Task<AjaxResult> Remove(string noticeIds)
        {
            var ids = noticeIds.Split(',').Select(long.Parse).ToArray();
            await _noticeService.DeleteAsync(ids);
            return Success();
        }
    }
}