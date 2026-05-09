using RuoYi.Common.Core;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysDictTypeService
    {
        private readonly ISqlSugarClient _db;
        public SysDictTypeService(ISqlSugarClient db) => _db = db;

        public async Task<PageResult<SysDictType>> SelectDictTypePageAsync(SysDictType dict)
        {
            var query = _db.Queryable<SysDictType>()
                .WhereIF(!string.IsNullOrEmpty(dict.DictName), d => d.DictName!.Contains(dict.DictName!))
                .WhereIF(!string.IsNullOrEmpty(dict.DictType), d => d.DictType!.Contains(dict.DictType!))
                .WhereIF(!string.IsNullOrEmpty(dict.Status), d => d.Status == dict.Status);

            var total = await query.Clone().CountAsync();
            var rows = await query.Clone().Skip((dict.PageNum - 1) * dict.PageSize).Take(dict.PageSize).ToListAsync();
            return new PageResult<SysDictType>(rows, total);
        }

        public async Task<SysDictType?> GetByIdAsync(long dictId)
        {
            return await _db.Queryable<SysDictType>().Where(d => d.DictId == dictId).FirstAsync();
        }

        public async Task<List<SysDictType>> GetListAsync()
        {
            return await _db.Queryable<SysDictType>().ToListAsync();
        }

        public async Task<int> AddAsync(SysDictType dict)
        {
            dict.CreateTime = DateTime.Now;
            return await _db.Insertable(dict).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(SysDictType dict)
        {
            dict.UpdateTime = DateTime.Now;
            return await _db.Updateable(dict).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(long[] dictIds)
        {
            return await _db.Deleteable<SysDictType>().Where(d => dictIds.Contains(d.DictId)).ExecuteCommandAsync();
        }
    }
}