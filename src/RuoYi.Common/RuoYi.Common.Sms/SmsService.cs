using Microsoft.Extensions.Logging;

namespace RuoYi.Common.Sms
{
    public class SmsService
    {
        private readonly ILogger<SmsService> _logger;

        public SmsService(ILogger<SmsService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendAsync(string phoneNumber, string templateCode, Dictionary<string, string> templateParams)
        {
            try
            {
                _logger.LogInformation("发送短信 - 手机号: {Phone}, 模板: {Template}, 参数: {Params}",
                    phoneNumber, templateCode,
                    string.Join(",", templateParams.Select(kv => $"{kv.Key}={kv.Value}")));

                // Integrate with actual SMS provider (Aliyun, Tencent, etc.)
                await Task.CompletedTask;

                _logger.LogInformation("短信发送成功: {Phone}", phoneNumber);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "短信发送失败: {Phone}", phoneNumber);
                return false;
            }
        }

        public async Task<bool> SendVerificationCodeAsync(string phoneNumber, string code)
        {
            var templateParams = new Dictionary<string, string>
            {
                { "code", code }
            };

            return await SendAsync(phoneNumber, "verification_code", templateParams);
        }
    }
}