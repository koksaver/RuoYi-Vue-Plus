namespace RuoYi.Modules.System.Models.Vo;

/// <summary>
/// 角色信息视图对象 sys_role
/// </summary>
public class SysRoleVo
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public long? RoleId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string? RoleName { get; set; }

    /// <summary>
    /// 角色权限字符串
    /// </summary>
    public string? RoleKey { get; set; }

    /// <summary>
    /// 显示顺序
    /// </summary>
    public int? RoleSort { get; set; }

    /// <summary>
    /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限 6：部门及以下或本人数据权限）
    /// </summary>
    public string? DataScope { get; set; }

    /// <summary>
    /// 菜单树选择项是否关联显示
    /// </summary>
    public bool? MenuCheckStrictly { get; set; }

    /// <summary>
    /// 部门树选择项是否关联显示
    /// </summary>
    public bool? DeptCheckStrictly { get; set; }

    /// <summary>
    /// 角色状态（0正常 1停用）
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 用户是否存在此角色标识 默认不存在
    /// </summary>
    public bool Flag { get; set; } = false;

    public bool IsSuperAdmin()
    {
        return RoleId == 1L;
    }
}
