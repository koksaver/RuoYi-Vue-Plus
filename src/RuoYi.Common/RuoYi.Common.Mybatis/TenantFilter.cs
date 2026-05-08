using SqlSugar;

namespace RuoYi.Common.Mybatis
{
    public class TenantFilter : ISugarQueryFilter
    {
        public string? TenantIdColumn { get; set; } = "tenant_id";

        public void ApplyFilter(SqlSugarProvider provider, ISugarQueryable queryable)
        {
            // Tenant filter logic - applied at query level
            // This is a stub for multi-tenant filtering
        }
    }
}