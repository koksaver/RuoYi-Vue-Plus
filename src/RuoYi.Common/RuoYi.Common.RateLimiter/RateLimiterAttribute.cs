using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RuoYi.Common.Redis;

namespace RuoYi.Common.RateLimiter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RateLimiterAttribute : Attribute, IAsyncActionFilter
    {
        public int Limit { get; set; } = 10;
        public int WindowSeconds { get; set; } = 1;
        public string Message { get; set; } = "请求过于频繁，请稍后再试";

        public RateLimiterAttribute() { }

        public RateLimiterAttribute(int limit, int windowSeconds = 1)
        {
            Limit = limit;
            WindowSeconds = windowSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var redisService = context.HttpContext.RequestServices.GetService<RedisService>();
            if (redisService == null)
            {
                await next();
                return;
            }

            var key = $"ratelimit:{context.HttpContext.Connection.RemoteIpAddress}:{context.HttpContext.Request.Path}";
            var currentCount = await redisService.IncrementAsync(key);

            if (currentCount == 1)
            {
                await redisService.ExpireAsync(key, TimeSpan.FromSeconds(WindowSeconds));
            }

            if (currentCount > Limit)
            {
                context.HttpContext.Response.StatusCode = 429;
                await context.HttpContext.Response.WriteAsJsonAsync(new { code = 429, msg = Message });
                return;
            }

            await next();
        }
    }
}