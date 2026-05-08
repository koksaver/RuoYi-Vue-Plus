using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_oper_log")]
    public class SysOperLog : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long OperId { get; set; }
        public string? Title { get; set; }
        public int BusinessType { get; set; }
        public string? Method { get; set; }
        public string? RequestMethod { get; set; }
        public int OperatorType { get; set; }
        public string? OperName { get; set; }
        public string? DeptName { get; set; }
        public string? OperUrl { get; set; }
        public string? OperIp { get; set; }
        public string? OperLocation { get; set; }
        public string? OperParam { get; set; }
        public string? JsonResult { get; set; }
        public int Status { get; set; }
        public string? ErrorMsg { get; set; }
        public long CostTime { get; set; }
    }
}