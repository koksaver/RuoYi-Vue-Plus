namespace RuoYi.Common.Core.Exceptions;

/// <summary>
/// 业务异常
/// </summary>
public sealed class ServiceException : Exception
{
    /// <summary>
    /// 错误码
    /// </summary>
    public int? Code { get; set; }

    /// <summary>
    /// 错误明细，内部调试错误
    /// </summary>
    public string? DetailMessage { get; set; }

    public ServiceException() { }

    public ServiceException(string message) : base(message) { }

    public ServiceException(string message, int code) : base(message)
    {
        Code = code;
    }

    public ServiceException(string message, params object[] args)
        : base(string.Format(message, args)) { }

    public ServiceException SetDetailMessage(string detailMessage)
    {
        DetailMessage = detailMessage;
        return this;
    }
}
