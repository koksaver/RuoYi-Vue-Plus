using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_post")]
    public class SysPost : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long PostId { get; set; }
        public string? PostCode { get; set; }
        public string? PostName { get; set; }
        public int PostSort { get; set; }
        public string? Status { get; set; }
    }
}