using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_role")]
    public class SysRole : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? RoleKey { get; set; }
        public int RoleSort { get; set; }
        public string? DataScope { get; set; }
        public string? Status { get; set; }
        public string? DelFlag { get; set; }

        [SugarColumn(IsIgnore = true)]
        public long[]? MenuIds { get; set; }

        [SugarColumn(IsIgnore = true)]
        public bool Flag { get; set; }
    }
}