using RuoYi.Common.Tenant;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace RuoYi.Modules.System.Domain;

/// <summary>
/// 部门表 sys_dept
/// </summary>
[Table("sys_dept")]
public class SysDept : TenantEntity
{
    /// <summary>
    /// 部门ID
    /// </summary>
    [Key]
    public long? DeptId { get; set; }

    /// <summary>
    /// 父部门ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 部门类别编码
    /// </summary>
    public string? DeptCategory { get; set; }

    /// <summary>
    /// 显示顺序
    /// </summary>
    public int? OrderNum { get; set; }

    /// <summary>
    /// 负责人
    /// </summary>
    public long? Leader { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 部门状态:0正常,1停用
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 删除标志（0代表存在 1代表删除）
    /// </summary>
    public string? DelFlag { get; set; }

    /// <summary>
    /// 祖级列表
    /// </summary>
    public string? Ancestors { get; set; }

    /// <summary>
    /// 子部门
    /// </summary>
    [NotMapped]
    public List<SysDept> Children { get; set; } = new();
}
