using SqlSugar;

namespace RuoYi.Common.Core
{
    public class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long? Id { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? Remark { get; set; }

        [SugarColumn(IsIgnore = true)]
        public int PageNum { get; set; } = 1;

        [SugarColumn(IsIgnore = true)]
        public int PageSize { get; set; } = 10;

        [SugarColumn(IsIgnore = true)]
        public string? OrderByColumn { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string? IsAsc { get; set; }
    }
}