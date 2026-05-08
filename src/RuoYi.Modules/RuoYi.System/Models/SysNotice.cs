using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_notice")]
    public class SysNotice : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long NoticeId { get; set; }
        public string? NoticeTitle { get; set; }
        public string? NoticeType { get; set; }
        public string? NoticeContent { get; set; }
        public string? Status { get; set; }
    }
}