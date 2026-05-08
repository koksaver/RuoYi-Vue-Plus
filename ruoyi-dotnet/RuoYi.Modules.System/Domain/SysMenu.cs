using RuoYi.Common.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace RuoYi.Modules.System.Domain;

/// <summary>
/// 菜单权限表 sys_menu
/// </summary>
[Table("sys_menu")]
public class SysMenu : BaseEntity
{
    /// <summary>
    /// 菜单ID
    /// </summary>
    [Key]
    public long? MenuId { get; set; }

    /// <summary>
    /// 父菜单ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string? MenuName { get; set; }

    /// <summary>
    /// 显示顺序
    /// </summary>
    public int? OrderNum { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    public string? Component { get; set; }

    /// <summary>
    /// 路由参数
    /// </summary>
    public string? QueryParam { get; set; }

    /// <summary>
    /// 是否为外链（0是 1否）
    /// </summary>
    public string? IsFrame { get; set; }

    /// <summary>
    /// 是否缓存（0缓存 1不缓存）
    /// </summary>
    public string? IsCache { get; set; }

    /// <summary>
    /// 类型（M目录 C菜单 F按钮）
    /// </summary>
    public string? MenuType { get; set; }

    /// <summary>
    /// 显示状态（0显示 1隐藏）
    /// </summary>
    public string? Visible { get; set; }

    /// <summary>
    /// 菜单状态（0正常 1停用）
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 权限字符串
    /// </summary>
    public string? Perms { get; set; }

    /// <summary>
    /// 菜单图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 父菜单名称
    /// </summary>
    [NotMapped]
    public string? ParentName { get; set; }

    /// <summary>
    /// 子菜单
    /// </summary>
    [NotMapped]
    public List<SysMenu> Children { get; set; } = new();
}
