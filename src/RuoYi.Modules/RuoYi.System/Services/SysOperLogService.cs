using RuoYi.Common.Core;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysOperLogService
    {
        private readonly ISqlSugarClient _db;
        public SysOperLogService(ISqlSugarClient db) => _db = db;

        public async Task<PageResult<SysOperLog>> SelectOperLogPageAsync(SysOperLog log)
        {
            var query = _db.Queryable<SysOperLog>()
                .WhereIF(!string.IsNullOrEmpty(log.Title), l => l.Title!.Contains(log.Title!))
                .WhereIF(!string.IsNullOrEmpty(log.OperName), l => l.OperName!.Contains(log.OperName!))
                .WhereIF(log.BusinessType > 0, l => l.BusinessType == log.BusinessType)
                .OrderBy(l => l.OperTime, OrderByType.Desc);

            var total = await query.Clone().CountAsync();
            var rows = await query.Clone().Skip((log.PageNum - 1) * log.PageSize).Take(log.PageSize).ToListAsync();
            return new PageResult<SysOperLog>(rows, total);
        }

        public async Task<int> AddAsync(SysOperLog log)
        {
            log.CreateTime = DateTime.Now;
            return await _db.Insertable(log).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(long[] operIds)
        {
            return await _db.Deleteable<SysOperLog>().Where(l => operIds.Contains(l.OperId)).ExecuteCommandAsync();
        }

        public async Task<int> CleanAsync()
        {
            return await _db.Deleteable<SysOperLog>().ExecuteCommandAsync();
        }
    }
}