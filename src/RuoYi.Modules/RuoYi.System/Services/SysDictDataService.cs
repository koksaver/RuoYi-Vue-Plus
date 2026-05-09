using RuoYi.Common.Core;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysDictDataService
    {
        private readonly ISqlSugarClient _db;
        public SysDictDataService(ISqlSugarClient db) => _db = db;

        public async Task<PageResult<SysDictData>> SelectDictDataPageAsync(SysDictData dict)
        {
            var query = _db.Queryable<SysDictData>()
                .WhereIF(!string.IsNullOrEmpty(dict.DictType), d => d.DictType == dict.DictType)
                .WhereIF(!string.IsNullOrEmpty(dict.DictLabel), d => d.DictLabel!.Contains(dict.DictLabel!))
                .WhereIF(!string.IsNullOrEmpty(dict.Status), d => d.Status == dict.Status)
                .OrderBy(d => d.DictSort);

            var total = await query.Clone().CountAsync();
            var rows = await query.Clone().Skip((dict.PageNum - 1) * dict.PageSize).Take(dict.PageSize).ToListAsync();
            return new PageResult<SysDictData>(rows, total);
        }

        public async Task<SysDictData?> GetByIdAsync(long dictCode)
        {
            return await _db.Queryable<SysDictData>().Where(d => d.DictCode == dictCode).FirstAsync();
        }

        public async Task<List<SysDictData>> GetDictDataByTypeAsync(string dictType)
        {
            return await _db.Queryable<SysDictData>()
                .Where(d => d.DictType == dictType && d.Status == "0")
                .OrderBy(d => d.DictSort)
                .ToListAsync();
        }

        public async Task<int> AddAsync(SysDictData dict)
        {
            dict.CreateTime = DateTime.Now;
            return await _db.Insertable(dict).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(SysDictData dict)
        {
            dict.UpdateTime = DateTime.Now;
            return await _db.Updateable(dict).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(long[] dictCodes)
        {
            return await _db.Deleteable<SysDictData>().Where(d => dictCodes.Contains(d.DictCode)).ExecuteCommandAsync();
        }
    }
}