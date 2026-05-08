using RuoYi.Common.Data;
using System.ComponentModel.DataAnnotations;

namespace RuoYi.Modules.System.Models.Bo;

/// <summary>
/// 用户信息业务对象 sys_user
/// </summary>
public class SysUserBo : BaseEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 用户账号
    /// </summary>
    [Required(ErrorMessage = "用户账号不能为空")]
    [StringLength(30, MinimumLength = 0, ErrorMessage = "用户账号长度不能超过30个字符")]
    public string? UserName { get; set; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    [Required(ErrorMessage = "用户昵称不能为空")]
    [StringLength(30, MinimumLength = 0, ErrorMessage = "用户昵称长度不能超过30个字符")]
    public string? NickName { get; set; }

    /// <summary>
    /// 用户类型（sys_user系统用户）
    /// </summary>
    public string? UserType { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    [StringLength(50, MinimumLength = 0, ErrorMessage = "邮箱长度不能超过50个字符")]
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
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 账号状态（0正常 1停用）
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 角色组
    /// </summary>
    [MinLength(1, ErrorMessage = "用户角色不能为空")]
    public long[]? RoleIds { get; set; }

    /// <summary>
    /// 岗位组
    /// </summary>
    public long[]? PostIds { get; set; }

    /// <summary>
    /// 数据权限 当前角色ID
    /// </summary>
    public long? RoleId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public string? UserIds { get; set; }

    /// <summary>
    /// 排除不查询的用户(工作流用)
    /// </summary>
    public string? ExcludeUserIds { get; set; }

    public bool IsSuperAdmin()
    {
        return UserId == 1L;
    }
}
