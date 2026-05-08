using RuoYi.Common.Tenant;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuoYi.Modules.System.Domain;

/// <summary>
/// 角色表 sys_role
/// </summary>
[Table("sys_role")]
public class SysRole : TenantEntity
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [Key]
    public long? RoleId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string? RoleName { get; set; }

    /// <summary>
    /// 角色权限
    /// </summary>
    public string? RoleKey { get; set; }

    /// <summary>
    /// 角色排序
    /// </summary>
    public int? RoleSort { get; set; }

    /// <summary>
    /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限 6：部门及以下或本人数据权限）
    /// </summary>
    public string? DataScope { get; set; }

    /// <summary>
    /// 菜单树选择项是否关联显示（ 0：父子不互相关联显示 1：父子互相关联显示）
    /// </summary>
    public bool? MenuCheckStrictly { get; set; }

    /// <summary>
    /// 部门树选择项是否关联显示（0：父子不互相关联显示 1：父子互相关联显示 ）
    /// </summary>
    public bool? DeptCheckStrictly { get; set; }

    /// <summary>
    /// 角色状态（0正常 1停用）
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 删除标志（0代表存在 1代表删除）
    /// </summary>
    public string? DelFlag { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    public bool IsSuperAdmin()
    {
        return RoleId == 1L;
    }
}
