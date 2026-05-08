using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_menu")]
    public class SysMenu : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long MenuId { get; set; }
        public string? MenuName { get; set; }
        public long? ParentId { get; set; }
        public int OrderNum { get; set; }
        public string? Path { get; set; }
        public string? Component { get; set; }
        public string? Query { get; set; }
        public string? IsFrame { get; set; }
        public string? IsCache { get; set; }
        public string? MenuType { get; set; }
        public string? Visible { get; set; }
        public string? Status { get; set; }
        public string? Perms { get; set; }
        public string? Icon { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<SysMenu>? Children { get; set; }
    }
}