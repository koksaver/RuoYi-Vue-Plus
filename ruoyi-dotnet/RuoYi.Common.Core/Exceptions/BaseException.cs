namespace RuoYi.Common.Core.Exceptions;

/// <summary>
/// 基础异常
/// </summary>
public class BaseException : Exception
{
    /// <summary>
    /// 所属模块
    /// </summary>
    public string? Module { get; set; }

    /// <summary>
    /// 错误码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 错误码对应的参数
    /// </summary>
    public object[]? Args { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? DefaultMessage { get; set; }

    public BaseException() { }

    public BaseException(string? module, string? code, object[]? args, string? defaultMessage)
        : base(defaultMessage)
    {
        Module = module;
        Code = code;
        Args = args;
        DefaultMessage = defaultMessage;
    }

    public BaseException(string? module, string? code, object[]? args)
        : this(module, code, args, null) { }

    public BaseException(string? module, string? defaultMessage)
        : this(module, null, null, defaultMessage) { }

    public BaseException(string? code, object[]? args)
        : this(null, code, args, null) { }

    public BaseException(string? defaultMessage)
        : this(null, null, null, defaultMessage) { }

    public override string Message => DefaultMessage ?? base.Message;
}
