using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_dept")]
    public class SysDept : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long DeptId { get; set; }
        public long? ParentId { get; set; }
        public string? DeptName { get; set; }
        public int OrderNum { get; set; }
        public string? Leader { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public string? DelFlag { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<SysDept>? Children { get; set; }
    }
}