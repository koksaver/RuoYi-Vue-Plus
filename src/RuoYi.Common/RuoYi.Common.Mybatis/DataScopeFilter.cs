namespace RuoYi.Common.Mybatis
{
    public class DataScopeFilter
    {
        public string? UserIdColumn { get; set; } = "create_by";
        public string? DeptIdColumn { get; set; } = "dept_id";

        public string BuildFilterSql(string? userId, string? deptId)
        {
            var conditions = new List<string>();

            if (!string.IsNullOrEmpty(userId))
            {
                conditions.Add($"{UserIdColumn} = '{userId}'");
            }

            if (!string.IsNullOrEmpty(deptId))
            {
                conditions.Add($"{DeptIdColumn} = '{deptId}'");
            }

            return conditions.Count > 0 ? string.Join(" OR ", conditions) : "1=1";
        }
    }
}