using Asp.Versioning;
using Azure.Core;
using Dnct.Application.Features.DatabaseSource.Query.GetDatabaseSourcesById;
using Dnct.Application.Features.Order.Queries.GetUserOrders;
using Dnct.Application.Features.Server.Query.GetServerInfo;
using Dnct.Application.Features.TreeView.Query.GetDatabasesByServerId;
using Dnct.Application.Features.TreeView.Query.GetTableInstanceByDatabaseSourceId;
using Dnct.Application.Features.TreeView.Query.GetTablesByDatabaseSourceIdId;
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
    [HttpGet("GetAllServers")]
    public async Task<IActionResult> GetAllServers()
    {
        var query = await sender.Send(new GetAllServersQuery());

        return base.OperationResult(query);
    }
    [HttpGet("GetDatabaseSourceById")]
    public async Task<IActionResult> GetDatabaseSourceById([FromQuery] GetDatabaseSourcesByIdQuery request)
    {
        var query = await sender.Send(request);

        return base.OperationResult(query);
    }
    [HttpGet("GetDatabasesByServerId")]
    public async Task<IActionResult> GetDatabasesByServerId([FromQuery] GetDatabasesByServerIdQuery request)
    {
        var query = await sender.Send(request);

        return base.OperationResult(query);
    }
    [HttpGet("GetTablesByDatabaseSourceId")]
    public async Task<IActionResult> GetTablesByDatabaseSourceId([FromQuery] GetTablesByDatabaseSourceIdQuery request)
    {
        var query = await sender.Send(request);

        return base.OperationResult(query);
    }

    [HttpGet("GetTableInstanceByDatabaseSourceId")]
    public async Task<IActionResult> GetTableInstanceByDatabaseSourceId([FromQuery] GetTableInstanceByDatabaseSourceIdQuery request)
    {
        var query = await sender.Send(request);

        return base.OperationResult(query);
    }
}