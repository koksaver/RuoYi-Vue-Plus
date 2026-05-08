using RuoYi.Common.Core;
using RuoYi.Common.Security;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysRoleService
    {
        private readonly ISqlSugarClient _db;

        public SysRoleService(ISqlSugarClient db) { _db = db; }

        public async Task<PageResult<SysRole>> GetPageAsync(SysRole role, int pageNum, int pageSize)
        {
            var query = _db.Queryable<SysRole>()
                .Where(r => r.DelFlag == "0")
                .WhereIF(!string.IsNullOrEmpty(role.RoleName), r => r.RoleName!.Contains(role.RoleName!))
                .WhereIF(!string.IsNullOrEmpty(role.RoleKey), r => r.RoleKey!.Contains(role.RoleKey!))
                .WhereIF(!string.IsNullOrEmpty(role.Status), r => r.Status == role.Status)
                .OrderBy(r => r.RoleSort);

            var total = await query.Clone().CountAsync();
            var rows = await query.Clone().Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageResult<SysRole>(rows, total);
        }

        public async Task<SysRole?> GetByIdAsync(long roleId) => await _db.Queryable<SysRole>().Where(r => r.RoleId == roleId).FirstAsync();

        public async Task<List<SysRole>> GetListAsync() => await _db.Queryable<SysRole>().Where(r => r.DelFlag == "0").ToListAsync();

        public async Task<int> AddAsync(SysRole role) { role.CreateTime = DateTime.Now; return await _db.Insertable(role).ExecuteCommandAsync(); }

        public async Task<int> UpdateAsync(SysRole role) { role.UpdateTime = DateTime.Now; return await _db.Updateable(role).ExecuteCommandAsync(); }

        public async Task<int> DeleteAsync(long[] roleIds) => await _db.Updateable<SysRole>()
            .SetColumns(r => new SysRole { DelFlag = "2", UpdateTime = DateTime.Now })
            .Where(r => roleIds.Contains(r.RoleId)).ExecuteCommandAsync();

        public async Task<bool> AssignMenuAsync(long roleId, long[] menuIds)
        {
            await _db.Deleteable<SysRoleMenu>().Where(rm => rm.RoleId == roleId).ExecuteCommandAsync();
            var list = menuIds.Select(m => new SysRoleMenu { RoleId = roleId, MenuId = m }).ToList();
            return await _db.Insertable(list).ExecuteCommandAsync() > 0;
        }

        public async Task<List<long>> GetMenuIdsAsync(long roleId)
        {
            return await _db.Queryable<SysRoleMenu>().Where(rm => rm.RoleId == roleId).Select(rm => rm.MenuId).ToListAsync();
        }
    }

    [SugarTable("sys_role_menu")]
    public class SysRoleMenu
    {
        public long RoleId { get; set; }
        public long MenuId { get; set; }
    }
}