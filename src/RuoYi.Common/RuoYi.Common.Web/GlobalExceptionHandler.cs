using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using RuoYi.Common.Core;

namespace RuoYi.Common.Web
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json;charset=utf-8";

            var result = new AjaxResult();

            if (exception is Core.Exceptions.ServiceException serviceEx)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = AjaxResult.Error(serviceEx.Code, serviceEx.Message);
            }
            else if (exception is Core.Exceptions.GlobalException globalEx)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = AjaxResult.Error(globalEx.Code, globalEx.Message);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = AjaxResult.Error("服务器内部错误: " + exception.Message);
            }

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }

    public static class GlobalExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}