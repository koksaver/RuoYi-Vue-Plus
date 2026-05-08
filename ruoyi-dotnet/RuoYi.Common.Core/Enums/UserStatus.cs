using System.ComponentModel;

namespace RuoYi.Common.Core.Enums;

/// <summary>
/// 用户状态
/// </summary>
public enum UserStatus
{
    /// <summary>
    /// 正常
    /// </summary>
    [Description("正常")]
    OK,

    /// <summary>
    /// 停用
    /// </summary>
    [Description("停用")]
    Disable,

    /// <summary>
    /// 删除
    /// </summary>
    [Description("删除")]
    Deleted
}
