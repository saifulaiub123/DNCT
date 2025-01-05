using Asp.Versioning;
using Dnct.Application.Features.TableColConfiguration.Query.GetAllTableColConfig;
using Dnct.Application.Features.UserQuery.Query.GetAllTableColConfig;
using Dnct.Application.Features.UserQuery.Query.GetUserQuery;
using Dnct.WebFramework.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Mediator;
using Dnct.Application.Features.TableColConfiguration.Commands.CreateMultiple;

namespace Dnct.Web.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/table-col-config")]

    [Authorize]
    public class TableColConfigurationController(Mediator.ISender sender) : BaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllTableColConfigQuery();
            var command = await sender.Send(query);

            return base.OperationResult(command);
        }
        [HttpGet("createMulti")]
        public async Task<IActionResult> CreateMulti([FromBody] CreateMultipleTblColConfigCommand command)
        {
             await sender.Send(command);

            return Ok();
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int tbleColConfigId, [FromQuery] int tableConfigId)
        {
            var command = new GetAllTableColConfigQuery();
            await sender.Send(command);

            return Ok();
        }
    }
}
