using Asp.Versioning;
using Dnct.Application.Features.LoadStragegy.Commands.Create.AddNewLoadStrategy;
using Dnct.Application.Features.LoadStragegy.Query.GetAllLoadStrategy;
using Dnct.Application.Features.UserQuery.Query.GetUserQuery;
using Dnct.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/load-strategy")]

    [Authorize]
    public class LoadStrategyController(ISender sender) : BaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllLoadStrategyQuery();
            var command = await sender.Send(query);

            return base.OperationResult(command);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddNewLoadStrategyCommand command)
        {
            var result = await sender.Send(command);

            return base.OperationResult(result);
        }
    }


}