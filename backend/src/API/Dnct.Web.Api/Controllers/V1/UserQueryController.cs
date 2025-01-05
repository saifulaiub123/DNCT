using Asp.Versioning;
using Dnct.Application.Features.Table.Commands.Create;
using Dnct.Application.Features.Table.Commands.DDLProcess;
using Dnct.Application.Features.UserQuery.Query.GetUserQuery;
using Dnct.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/user-query")]

    [Authorize]
    public class UserQueryController(ISender sender) : BaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetUserQueryQuery();
            var command = await sender.Send(query);

            return base.OperationResult(command);
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate([FromBody] DeleteUserQueryCommand command)
        {
            var result = await sender.Send(command);

            return base.OperationResult(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> CreateOrUpdate([FromRoute] int userQueryId, [FromQuery] int tableConfigId)
        {
            var command = new DeleteUserQueryCommand
            {
                UserQueryId = userQueryId,
                TableConfigId = tableConfigId
            };
            var result = await sender.Send(command);

            return base.OperationResult(result);
        }
    }
}