using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_user")]
    public class SysUser : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Sex { get; set; }
        public string? Avatar { get; set; }
        public string? Password { get; set; }
        public string? Status { get; set; }
        public string? DelFlag { get; set; }
        public string? LoginIp { get; set; }
        public DateTime? LoginDate { get; set; }
        public long? DeptId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public long[]? RoleIds { get; set; }

        [SugarColumn(IsIgnore = true)]
        public long[]? PostIds { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string? RoleName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string? DeptName { get; set; }
    }
}