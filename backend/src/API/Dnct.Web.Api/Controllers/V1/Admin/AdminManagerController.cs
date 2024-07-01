using Asp.Versioning;
using Dnct.Application.Features.Admin.Commands.AddAdminCommand;
using Dnct.Application.Features.Admin.Queries.GetToken;
using Dnct.WebFramework.BaseController;
using Dnct.WebFramework.WebExtensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1.Admin
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/AdminManager")]
    public class AdminManagerController(ISender sender) : BaseController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> AdminLogin(AdminGetTokenQuery model)
        {
            var query = await sender.Send(model);

            return base.OperationResult(query);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("NewAdmin")]
        public async Task<IActionResult> AddNewAdmin(AddAdminCommand model)
        {
            var commandResult = await sender.Send(model);

            return base.OperationResult(commandResult);
        }
    }
}
