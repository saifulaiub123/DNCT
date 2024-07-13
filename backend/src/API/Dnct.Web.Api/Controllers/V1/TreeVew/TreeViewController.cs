using Asp.Versioning;
using Dnct.Application.Features.Order.Queries.GetUserOrders;
using Dnct.Application.Features.Server.Query.GetServerInfo;
using Dnct.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1.Order;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/tree-view")]
//[Authorize]
public class TreeViewController(ISender sender) : BaseController
{
    [HttpGet("getAllServers")]
    public async Task<IActionResult> GetAllServers()
    {
        var query = await sender.Send(new GetAllServersQuery());

        return base.OperationResult(query);
    }
}