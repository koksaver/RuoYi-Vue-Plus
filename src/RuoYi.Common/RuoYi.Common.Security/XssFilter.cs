using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace RuoYi.Common.Security
{
    public class XssFilter
    {
        private readonly RequestDelegate _next;

        public XssFilter(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "PATCH")
            {
                context.Request.EnableBuffering();
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                if (!string.IsNullOrEmpty(body) && ContainsXss(body))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Request contains XSS injection attempt");
                    return;
                }
            }

            await _next(context);
        }

        private static bool ContainsXss(string input)
        {
            var xssPattern = @"<script[^>]*>.*?</script|on\w+\s*=|javascript:|alert\(|prompt\(|confirm\(|<iframe|<embed|<object|<link|<style|<img[^>]*onerror";
            return Regex.IsMatch(input, xssPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        public static string CleanXss(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = Regex.Replace(input, @"<script[^>]*>.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            input = Regex.Replace(input, @"on\w+\s*=\s*""[^""]*""", "", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"on\w+\s*=\s*'[^']*'", "", RegexOptions.IgnoreCase);
            input = input.Replace("javascript:", "").Replace("JAVASCRIPT:", "");

            return input;
        }
    }

    public static class XssFilterExtensions
    {
        public static IApplicationBuilder UseXssFilter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<XssFilter>();
        }
    }
}