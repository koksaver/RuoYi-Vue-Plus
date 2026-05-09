using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;

namespace RuoYi.System.Controllers
{
    [ApiController]
    [Authorize]
    [Route("system/user/profile")]
    public class SysProfileController : BaseController
    {
        [HttpGet]
        public AjaxResult GetProfile()
        {
            return Success(new { });
        }

        [HttpPut]
        public AjaxResult UpdateProfile([FromBody] object profile)
        {
            return Success();
        }

        [HttpPut("avatar")]
        public AjaxResult UpdateAvatar([FromBody] object avatar)
        {
            return Success();
        }

        [HttpPut("password")]
        public AjaxResult UpdatePassword([FromBody] object password)
        {
            return Success();
        }
    }
}