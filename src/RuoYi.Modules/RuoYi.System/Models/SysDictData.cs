using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_dict_data")]
    public class SysDictData : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long DictCode { get; set; }
        public string? DictSort { get; set; }
        public string? DictLabel { get; set; }
        public string? DictValue { get; set; }
        public string? DictType { get; set; }
        public string? CssClass { get; set; }
        public string? ListClass { get; set; }
        public string? IsDefault { get; set; }
        public string? Status { get; set; }
    }
}