using RuoYi.Common.Core;
using RuoYi.System.Models;
using SqlSugar;

namespace RuoYi.System.Services
{
    public class SysMenuService
    {
        private readonly ISqlSugarClient _db;

        public SysMenuService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<List<SysMenu>> SelectMenuListAsync(SysMenu menu)
        {
            var query = _db.Queryable<SysMenu>()
                .WhereIF(!string.IsNullOrEmpty(menu.MenuName), m => m.MenuName!.Contains(menu.MenuName!))
                .WhereIF(!string.IsNullOrEmpty(menu.Status), m => m.Status == menu.Status)
                .OrderBy(m => m.ParentId).OrderBy(m => m.OrderNum);
            return await query.ToListAsync();
        }

        public async Task<List<SysMenu>> SelectMenuTreeListAsync()
        {
            return await _db.Queryable<SysMenu>()
                .Where(m => m.MenuType != "F")
                .OrderBy(m => m.ParentId).OrderBy(m => m.OrderNum)
                .ToListAsync();
        }

        public List<SysMenu> BuildMenuTree(List<SysMenu> menus, long? parentId = 0)
        {
            return menus.Where(m => m.ParentId == parentId).Select(m =>
            {
                m.Children = BuildMenuTree(menus, m.MenuId);
                return m;
            }).ToList();
        }

        public async Task<SysMenu?> GetByIdAsync(long menuId)
        {
            return await _db.Queryable<SysMenu>().Where(m => m.MenuId == menuId).FirstAsync();
        }

        public async Task<int> AddAsync(SysMenu menu)
        {
            menu.CreateTime = DateTime.Now;
            return await _db.Insertable(menu).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(SysMenu menu)
        {
            menu.UpdateTime = DateTime.Now;
            return await _db.Updateable(menu).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(long menuId)
        {
            return await _db.Deleteable<SysMenu>().Where(m => m.MenuId == menuId).ExecuteCommandAsync();
        }
    }
}