using System.ComponentModel;

namespace RuoYi.Common.Core.Enums;

/// <summary>
/// 用户类型
/// </summary>
public enum UserType
{
    /// <summary>
    /// 后台系统用户
    /// </summary>
    [Description("后台系统用户")]
    SysUser,

    /// <summary>
    /// 移动客户端用户
    /// </summary>
    [Description("移动客户端用户")]
    AppUser
}
