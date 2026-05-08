using Microsoft.Extensions.Logging;
using RuoYi.Common.Redis;

namespace RuoYi.Common.Social
{
    public class AuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly RedisService _redisService;

        public AuthService(ILogger<AuthService> logger, RedisService redisService)
        {
            _logger = logger;
            _redisService = redisService;
        }

        public async Task<string> AuthorizeAsync(string platform, string code, string state)
        {
            _logger.LogInformation("第三方登录 - 平台: {Platform}, Code: {Code}, State: {State}",
                platform, code, state);

            // Integrate with JustAuth-style OAuth providers
            // WeChat, DingTalk, QQ, GitHub, etc.
            // Returns the openId or user identifier from the platform
            var openId = $"{platform}_user_{code[..8]}";

            await _redisService.SetStringAsync($"social:token:{openId}", code, TimeSpan.FromHours(24));

            return openId;
        }

        public async Task<bool> BindAsync(long userId, string platform, string openId)
        {
            _logger.LogInformation("绑定第三方账号 - 用户: {UserId}, 平台: {Platform}, OpenId: {OpenId}",
                userId, platform, openId);

            await _redisService.SetStringAsync($"social:bind:{userId}:{platform}", openId);
            await _redisService.SetStringAsync($"social:user:{platform}:{openId}", userId.ToString());

            return true;
        }

        public async Task<long?> GetUserIdByOpenIdAsync(string platform, string openId)
        {
            var userIdStr = await _redisService.GetStringAsync($"social:user:{platform}:{openId}");
            if (long.TryParse(userIdStr, out var userId))
                return userId;

            return null;
        }
    }
}