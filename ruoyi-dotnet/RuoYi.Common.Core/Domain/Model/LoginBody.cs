using System.ComponentModel.DataAnnotations;

namespace RuoYi.Common.Core.Domain.Model;

/// <summary>
/// 用户登录对象
/// </summary>
public class LoginBody
{
    /// <summary>
    /// 客户端id
    /// </summary>
    [Required(ErrorMessage = "Client ID is required")]
    public string? ClientId { get; set; }

    /// <summary>
    /// 授权类型
    /// </summary>
    [Required(ErrorMessage = "Grant type is required")]
    public string? GrantType { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public string? TenantId { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 唯一标识
    /// </summary>
    public string? Uuid { get; set; }
}
