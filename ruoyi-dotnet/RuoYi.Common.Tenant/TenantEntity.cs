using RuoYi.Common.Data;

namespace RuoYi.Common.Tenant;

/// <summary>
/// 租户基类
/// </summary>
public class TenantEntity : BaseEntity
{
    /// <summary>
    /// 租户编号
    /// </summary>
    public string? TenantId { get; set; }
}
