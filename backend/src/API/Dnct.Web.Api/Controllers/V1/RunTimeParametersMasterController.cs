using Asp.Versioning;
using Dnct.Application.Features.LoadStragegy.Commands.Create.AddNewLoadStrategy;
using Dnct.Application.Features.LoadStragegy.Query.GetAllLoadStrategy;
using Dnct.Application.Features.RunTimeParametersMaster.Commands.Create;
using Dnct.Application.Features.RunTimeParametersMaster.Query.GetAll;
using Dnct.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1
{

    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/run-time-master")]

    [Authorize]
    public class RunTimeParametersMasterController(ISender sender) : BaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRunTimeParametersMasterQuery query)
        {
            var result = await sender.Send(query);

            return base.OperationResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateRunTimeParametersMasterCommand command)
        {
            var result = await sender.Send(command);

            return base.OperationResult(result);
        }
    }


}