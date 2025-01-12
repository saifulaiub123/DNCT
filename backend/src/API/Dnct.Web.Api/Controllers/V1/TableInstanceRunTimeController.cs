using Asp.Versioning;
using Dnct.Application.Features.TableInstanceRunTime.Commands.CreateTableInstanceRunTime;
using Dnct.Application.Features.TableInstanceRunTime.Query;
using Dnct.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/table-instance-run-time")]

    [Authorize]
    public class TableInstanceRunTimeController(ISender sender) : BaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTableInstanceRunTimeQuery query)
        {
            var result = await sender.Send(query);

            return base.OperationResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateTableInstanceRunTimeCommand command)
        {
            var result = await sender.Send(command);

            return base.OperationResult(result);
        }
    }
}
