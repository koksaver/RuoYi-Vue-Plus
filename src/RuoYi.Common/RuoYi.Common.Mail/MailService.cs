using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Logging;

namespace RuoYi.Common.Mail
{
    public class MailService
    {
        private readonly ILogger<MailService> _logger;

        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendAsync(MailMessage message, MailConfig config)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(config.SenderName ?? config.SenderEmail, config.SenderEmail));
                mimeMessage.To.Add(new MailboxAddress(message.ToName ?? message.To, message.To));
                mimeMessage.Subject = message.Subject;

                var bodyBuilder = new BodyBuilder();
                if (message.IsHtml)
                {
                    bodyBuilder.HtmlBody = message.Body;
                }
                else
                {
                    bodyBuilder.TextBody = message.Body;
                }

                if (message.Attachments != null)
                {
                    foreach (var attachment in message.Attachments)
                    {
                        await bodyBuilder.Attachments.AddAsync(attachment.FileName, attachment.Data, attachment.ContentType);
                    }
                }

                mimeMessage.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(config.Host, config.Port, config.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(config.UserName, config.Password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);

                _logger.LogInformation("邮件发送成功: {To}, 主题: {Subject}", message.To, message.Subject);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "邮件发送失败: {To}, 主题: {Subject}", message.To, message.Subject);
                return false;
            }
        }
    }

    public class MailMessage
    {
        public string To { get; set; } = string.Empty;
        public string? ToName { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsHtml { get; set; } = true;
        public List<MailAttachment>? Attachments { get; set; }
    }

    public class MailAttachment
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Data { get; set; } = Array.Empty<byte>();
        public string ContentType { get; set; } = "application/octet-stream";
    }

    public class MailConfig
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string? SenderName { get; set; }
        public bool UseSsl { get; set; } = false;
    }
}