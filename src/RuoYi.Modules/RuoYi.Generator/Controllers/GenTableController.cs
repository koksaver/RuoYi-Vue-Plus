using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuoYi.Common.Core;

namespace RuoYi.Generator.Controllers
{
    [ApiController]
    [Authorize]
    [Route("tool/gen")]
    public class GenTableController : BaseController
    {
        [HttpGet("list")]
        public AjaxResult List()
        {
            return Success("代码生成器就绪");
        }

        [HttpGet("{tableId}")]
        public AjaxResult Get(long tableId)
        {
            return Success(new { tableId });
        }

        [HttpPost]
        public AjaxResult Add([FromBody] object table)
        {
            return Success();
        }

        [HttpPut]
        public AjaxResult Edit([FromBody] object table)
        {
            return Success();
        }

        [HttpDelete("{tableIds}")]
        public AjaxResult Remove(string tableIds)
        {
            return Success();
        }

        [HttpPost("genCode/{tableName}")]
        public AjaxResult GenCode(string tableName)
        {
            return Success("代码生成成功");
        }
    }
}