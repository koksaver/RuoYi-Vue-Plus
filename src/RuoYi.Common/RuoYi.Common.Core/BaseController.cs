using Microsoft.AspNetCore.Mvc;

namespace RuoYi.Common.Core
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected AjaxResult Success(object? data = null)
        {
            return AjaxResult.Success(data);
        }

        protected AjaxResult Success(string msg, object? data = null)
        {
            return AjaxResult.Success(msg, data);
        }

        protected AjaxResult Error(string msg)
        {
            return AjaxResult.Error(msg);
        }

        protected AjaxResult Error(int code, string msg)
        {
            return AjaxResult.Error(code, msg);
        }

        protected AjaxResult Warn(string msg)
        {
            return AjaxResult.Warn(msg);
        }

        protected PageResult<T> GetPageResult<T>(List<T> rows, long total)
        {
            return new PageResult<T>(rows, total);
        }
    }
}