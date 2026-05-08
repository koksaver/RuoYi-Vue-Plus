using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_logininfor")]
    public class SysLoginInfo : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long InfoId { get; set; }
        public string? UserName { get; set; }
        public string? Ipaddr { get; set; }
        public string? LoginLocation { get; set; }
        public string? Browser { get; set; }
        public string? Os { get; set; }
        public string? Status { get; set; }
        public string? Msg { get; set; }
        public DateTime? LoginTime { get; set; }
    }
}