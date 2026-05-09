using RuoYi.Common.Core;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysDeptService
    {
        private readonly ISqlSugarClient _db;

        public SysDeptService(ISqlSugarClient db) => _db = db;

        public async Task<List<SysDept>> SelectDeptListAsync(SysDept dept)
        {
            return await _db.Queryable<SysDept>()
                .Where(d => d.DelFlag == "0")
                .WhereIF(!string.IsNullOrEmpty(dept.Status), d => d.Status == dept.Status)
                .WhereIF(!string.IsNullOrEmpty(dept.DeptName), d => d.DeptName!.Contains(dept.DeptName!))
                .OrderBy(d => d.ParentId).OrderBy(d => d.OrderNum)
                .ToListAsync();
        }

        public List<SysDept> BuildDeptTree(List<SysDept> depts, long? parentId = 0)
        {
            return depts.Where(d => d.ParentId == parentId).Select(d =>
            {
                d.Children = BuildDeptTree(depts, d.DeptId);
                return d;
            }).ToList();
        }

        public async Task<SysDept?> GetByIdAsync(long deptId)
        {
            return await _db.Queryable<SysDept>().Where(d => d.DeptId == deptId).FirstAsync();
        }

        public async Task<int> AddAsync(SysDept dept)
        {
            dept.CreateTime = DateTime.Now;
            dept.DelFlag = "0";
            return await _db.Insertable(dept).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(SysDept dept)
        {
            dept.UpdateTime = DateTime.Now;
            return await _db.Updateable(dept).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(long deptId)
        {
            return await _db.Updateable<SysDept>()
                .SetColumns(d => new SysDept { DelFlag = "2", UpdateTime = DateTime.Now })
                .Where(d => d.DeptId == deptId)
                .ExecuteCommandAsync();
        }
    }
}