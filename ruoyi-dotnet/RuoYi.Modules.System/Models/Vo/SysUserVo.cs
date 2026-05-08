using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace RuoYi.Modules.System.Models.Vo;

/// <summary>
/// 用户信息视图对象 sys_user
/// </summary>
public class SysUserVo
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public string? TenantId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 用户账号
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 用户类型（sys_user系统用户）
    /// </summary>
    public string? UserType { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    public string? Phonenumber { get; set; }

    /// <summary>
    /// 用户性别（0男 1女 2未知）
    /// </summary>
    public string? Sex { get; set; }

    /// <summary>
    /// 头像地址
    /// </summary>
    public long? Avatar { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [JsonIgnore]
    public string? Password { get; set; }

    /// <summary>
    /// 账号状态（0正常 1停用）
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 最后登录IP
    /// </summary>
    public string? LoginIp { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime? LoginDate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 部门名
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 角色对象
    /// </summary>
    public List<SysRoleVo>? Roles { get; set; }

    /// <summary>
    /// 角色组
    /// </summary>
    public long[]? RoleIds { get; set; }

    /// <summary>
    /// 岗位组
    /// </summary>
    public long[]? PostIds { get; set; }

    /// <summary>
    /// 数据权限 当前角色ID
    /// </summary>
    public long? RoleId { get; set; }
}
