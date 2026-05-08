using SqlSugar;
using RuoYi.Common.Core;

namespace RuoYi.System.Models
{
    [SugarTable("sys_dict_type")]
    public class SysDictType : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long DictId { get; set; }
        public string? DictName { get; set; }
        public string? DictType { get; set; }
        public string? Status { get; set; }
    }
}