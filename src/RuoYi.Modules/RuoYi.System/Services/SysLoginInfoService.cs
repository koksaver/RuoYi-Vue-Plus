using RuoYi.Common.Core;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysLoginInfoService
    {
        private readonly ISqlSugarClient _db;
        public SysLoginInfoService(ISqlSugarClient db) => _db = db;

        public async Task<PageResult<SysLoginInfo>> SelectLoginInfoPageAsync(SysLoginInfo info)
        {
            var query = _db.Queryable<SysLoginInfo>()
                .WhereIF(!string.IsNullOrEmpty(info.UserName), l => l.UserName!.Contains(info.UserName!))
                .WhereIF(!string.IsNullOrEmpty(info.Ipaddr), l => l.Ipaddr!.Contains(info.Ipaddr!))
                .WhereIF(!string.IsNullOrEmpty(info.Status), l => l.Status == info.Status)
                .OrderBy(l => l.LoginTime, OrderByType.Desc);

            var total = await query.Clone().CountAsync();
            var rows = await query.Clone().Skip((info.PageNum - 1) * info.PageSize).Take(info.PageSize).ToListAsync();
            return new PageResult<SysLoginInfo>(rows, total);
        }

        public async Task<int> AddAsync(SysLoginInfo info)
        {
            info.CreateTime = DateTime.Now;
            return await _db.Insertable(info).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(long[] infoIds)
        {
            return await _db.Deleteable<SysLoginInfo>().Where(l => infoIds.Contains(l.InfoId)).ExecuteCommandAsync();
        }

        public async Task<int> CleanAsync()
        {
            return await _db.Deleteable<SysLoginInfo>().ExecuteCommandAsync();
        }
    }
}