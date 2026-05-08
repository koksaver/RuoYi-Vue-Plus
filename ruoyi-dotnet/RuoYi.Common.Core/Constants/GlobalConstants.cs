namespace RuoYi.Common.Core.Constants;

/// <summary>
/// 全局的key常量 (业务无关的key)
/// </summary>
public static class GlobalConstants
{
    /// <summary>
    /// 全局 redis key (业务无关的key)
    /// </summary>
    public const string GLOBAL_REDIS_KEY = "global:";

    /// <summary>
    /// 验证码 redis key
    /// </summary>
    public const string CAPTCHA_CODE_KEY = GLOBAL_REDIS_KEY + "captcha_codes:";

    /// <summary>
    /// 防重提交 redis key
    /// </summary>
    public const string REPEAT_SUBMIT_KEY = GLOBAL_REDIS_KEY + "repeat_submit:";

    /// <summary>
    /// 限流 redis key
    /// </summary>
    public const string RATE_LIMIT_KEY = GLOBAL_REDIS_KEY + "rate_limit:";

    /// <summary>
    /// 三方认证 redis key
    /// </summary>
    public const string SOCIAL_AUTH_CODE_KEY = GLOBAL_REDIS_KEY + "social_auth_codes:";
}
