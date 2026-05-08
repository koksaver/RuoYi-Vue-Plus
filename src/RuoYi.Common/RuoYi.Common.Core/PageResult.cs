namespace RuoYi.Common.Core
{
    public class PageResult<T>
    {
        public int Code { get; set; } = 200;
        public string? Msg { get; set; } = "查询成功";
        public long Total { get; set; }
        public List<T>? Rows { get; set; }

        public PageResult() { }

        public PageResult(List<T> rows, long total)
        {
            Rows = rows;
            Total = total;
        }

        public static PageResult<T> Empty()
        {
            return new PageResult<T> { Rows = new List<T>(), Total = 0 };
        }
    }
}