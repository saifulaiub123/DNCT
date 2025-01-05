using Asp.Versioning;
using Dnct.Application.Features.UserQuery.Query.GetAllTableColConfig;
using Dnct.Application.Features.UserQuery.Query.GetUserQuery;
using Dnct.WebFramework.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Mediator;

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
    }
}
