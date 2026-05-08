using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RuoYi.Common.Redis;

namespace RuoYi.Common.Idempotent
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IdempotentAttribute : Attribute, IAsyncActionFilter
    {
        public string Key { get; set; } = string.Empty;
        public int ExpireSeconds { get; set; } = 5;
        public string Message { get; set; } = "请勿重复提交";

        public IdempotentAttribute() { }

        public IdempotentAttribute(string key, int expireSeconds = 5)
        {
            Key = key;
            ExpireSeconds = expireSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var redisService = context.HttpContext.RequestServices.GetService<RedisService>();
            if (redisService == null)
            {
                await next();
                return;
            }

            var lockKey = $"idempotent:{Key}:{context.HttpContext.Connection.RemoteIpAddress}";
            var lockValue = Guid.NewGuid().ToString();
            var expiry = TimeSpan.FromSeconds(ExpireSeconds);

            var acquired = await redisService.SetStringAsync(lockKey, lockValue, expiry);
            if (!acquired)
            {
                context.HttpContext.Response.StatusCode = 429;
                await context.HttpContext.Response.WriteAsJsonAsync(new { code = 429, msg = Message });
                return;
            }

            await next();
        }
    }
}