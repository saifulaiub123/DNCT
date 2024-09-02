using Asp.Versioning;
using Dnct.Application.Features.DatabaseSource.Commands.CreateNewObject;
using Dnct.Application.Features.DatabaseSource.Query.GetDatabaseSourcesById;
using Dnct.Application.Features.Server.Query.GetServerInfo;
using Dnct.Application.Features.Table.Commands.Create;
using Dnct.Application.Features.TreeView.Query.GetDatabasesByServerId;
using Dnct.Application.Features.TreeView.Query.GetTableInstanceByDatabaseSourceId;
using Dnct.Application.Features.TreeView.Query.GetTablesByDatabaseSourceIdId;
using Dnct.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/database-source")]
    public class DatabaseSourceController(ISender sender) : BaseController
    {

        [HttpPost("create-new-object")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateNewObjectCommand command)
        {
            var res = await sender.Send(command);

            return base.OperationResult(res);
        }
        //[HttpGet("GetAllServers")]
        //public async Task<IActionResult> GetAllServers()
        //{
        //    var query = await sender.Send(new GetAllServersQuery());

        //    return base.OperationResult(query);
        //}
        //[HttpGet("GetDatabaseSourceById")]
        //public async Task<IActionResult> GetDatabaseSourceById([FromQuery] GetDatabaseSourcesByIdQuery request)
        //{
        //    var query = await sender.Send(request);

        //    return base.OperationResult(query);
        //}
        //[HttpGet("GetDatabasesByServerId")]
        //public async Task<IActionResult> GetDatabasesByServerId([FromQuery] GetDatabaseSourcesByIdQuery request)
        //{
        //    var query = await sender.Send(request);

        //    return base.OperationResult(query);
        //}
        //[HttpGet("GetTablesByDatabaseSourceId")]
        //public async Task<IActionResult> GetTablesByDatabaseSourceId([FromQuery] GetTablesByDatabaseSourceIdQuery request)
        //{
        //    var query = await sender.Send(request);

        //    return base.OperationResult(query);
        //}

        //[HttpGet("GetTableInstanceByDatabaseSourceId")]
        //public async Task<IActionResult> GetTableInstanceByDatabaseSourceId([FromQuery] GetTableInstanceByDatabaseSourceIdQuery request)
        //{
        //    var query = await sender.Send(request);

        //    return base.OperationResult(query);
        //}
    }
}