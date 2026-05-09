using RuoYi.Common.Core;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysNoticeService
    {
        private readonly ISqlSugarClient _db;
        public SysNoticeService(ISqlSugarClient db) => _db = db;

        public async Task<PageResult<SysNotice>> SelectNoticePageAsync(SysNotice notice)
        {
            var query = _db.Queryable<SysNotice>()
                .WhereIF(!string.IsNullOrEmpty(notice.NoticeTitle), n => n.NoticeTitle!.Contains(notice.NoticeTitle!))
                .WhereIF(!string.IsNullOrEmpty(notice.NoticeType), n => n.NoticeType == notice.NoticeType)
                .OrderBy(n => n.CreateTime, OrderByType.Desc);

            var total = await query.Clone().CountAsync();
            var rows = await query.Clone().Skip((notice.PageNum - 1) * notice.PageSize).Take(notice.PageSize).ToListAsync();
            return new PageResult<SysNotice>(rows, total);
        }

        public async Task<SysNotice?> GetByIdAsync(long noticeId)
        {
            return await _db.Queryable<SysNotice>().Where(n => n.NoticeId == noticeId).FirstAsync();
        }

        public async Task<int> AddAsync(SysNotice notice)
        {
            notice.CreateTime = DateTime.Now;
            return await _db.Insertable(notice).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(SysNotice notice)
        {
            notice.UpdateTime = DateTime.Now;
            return await _db.Updateable(notice).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(long[] noticeIds)
        {
            return await _db.Deleteable<SysNotice>().Where(n => noticeIds.Contains(n.NoticeId)).ExecuteCommandAsync();
        }
    }
}