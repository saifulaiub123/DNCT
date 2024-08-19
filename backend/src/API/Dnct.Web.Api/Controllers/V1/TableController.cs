using Asp.Versioning;
using Dnct.Application.Features.Order.Commands;
using Dnct.Application.Features.Order.Queries.GetUserOrders;
using Dnct.Application.Features.Table.Commands.DDLProcess;
using Dnct.Domain.Entities.User;
using Dnct.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/table")]

    [Authorize]
    public class TableController(ISender sender) : BaseController
    {
        [HttpPost("CreateNewTable")]
        public async Task<IActionResult> CreateNewOrder(AddOrderCommand model)
        {
            model.UserId = base.UserId;
            var command = await sender.Send(model);

            return base.OperationResult(command);
        }

        [HttpPost("process-ddl")]
        public async Task<IActionResult> ProcessDDL(ProcessDDLCommand model)
        {
            var command = await sender.Send(model);

            return base.OperationResult(command);
        }
    }
}
