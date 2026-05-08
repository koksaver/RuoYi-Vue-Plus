using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RuoYi.Common.Core;
using RuoYi.Common.Core.Enums;

namespace RuoYi.Common.Log
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OperLogAttribute : Attribute, IAsyncActionFilter
    {
        public string Title { get; set; } = string.Empty;
        public BusinessType BusinessType { get; set; } = BusinessType.OTHER;
        public OperatorType OperatorType { get; set; } = OperatorType.MANAGE;
        public bool IsSaveRequestData { get; set; } = true;
        public bool IsSaveResponseData { get; set; } = true;

        public OperLogAttribute() { }

        public OperLogAttribute(string title, BusinessType businessType = BusinessType.OTHER)
        {
            Title = title;
            BusinessType = businessType;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<OperLogAttribute>>();

            logger.LogInformation("[操作日志] Title: {Title}, BusinessType: {BusinessType}, OperatorType: {OperatorType}",
                Title, BusinessType, OperatorType);

            await next();
        }
    }
}