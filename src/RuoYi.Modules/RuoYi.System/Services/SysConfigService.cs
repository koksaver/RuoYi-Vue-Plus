using RuoYi.Common.Core;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysConfigService
    {
        private readonly ISqlSugarClient _db;
        public SysConfigService(ISqlSugarClient db) => _db = db;

        public async Task<PageResult<SysConfig>> SelectConfigPageAsync(SysConfig config)
        {
            var query = _db.Queryable<SysConfig>()
                .WhereIF(!string.IsNullOrEmpty(config.ConfigName), c => c.ConfigName!.Contains(config.ConfigName!))
                .WhereIF(!string.IsNullOrEmpty(config.ConfigKey), c => c.ConfigKey!.Contains(config.ConfigKey!))
                .WhereIF(!string.IsNullOrEmpty(config.ConfigType), c => c.ConfigType == config.ConfigType);

            var total = await query.Clone().CountAsync();
            var rows = await query.Clone().Skip((config.PageNum - 1) * config.PageSize).Take(config.PageSize).ToListAsync();
            return new PageResult<SysConfig>(rows, total);
        }

        public async Task<SysConfig?> GetByIdAsync(long configId)
        {
            return await _db.Queryable<SysConfig>().Where(c => c.ConfigId == configId).FirstAsync();
        }

        public async Task<SysConfig?> GetByKeyAsync(string configKey)
        {
            return await _db.Queryable<SysConfig>().Where(c => c.ConfigKey == configKey).FirstAsync();
        }

        public async Task<int> AddAsync(SysConfig config)
        {
            config.CreateTime = DateTime.Now;
            return await _db.Insertable(config).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(SysConfig config)
        {
            config.UpdateTime = DateTime.Now;
            return await _db.Updateable(config).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(long[] configIds)
        {
            return await _db.Deleteable<SysConfig>().Where(c => configIds.Contains(c.ConfigId)).ExecuteCommandAsync();
        }
    }
}