using RuoYi.Common.Tenant;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuoYi.Modules.System.Domain;

/// <summary>
/// 用户对象 sys_user
/// </summary>
[Table("sys_user")]
public class SysUser : TenantEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Key]
    public long? UserId { get; set; }

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
    /// 用户性别
    /// </summary>
    public string? Sex { get; set; }

    /// <summary>
    /// 用户头像
    /// </summary>
    public long? Avatar { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 账号状态（0正常 1停用）
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 删除标志（0代表存在 1代表删除）
    /// </summary>
    public string? DelFlag { get; set; }

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

    public bool IsSuperAdmin()
    {
        return UserId == 1L; // SystemConstants.SUPER_ADMIN_ID is usually 1L
    }
}
