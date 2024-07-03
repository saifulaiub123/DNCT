using Asp.Versioning;
using Dnct.Application.Features.Identity.Commands.Create;
using Dnct.Application.Features.Identity.Commands.RefreshUserTokenCommand;
using Dnct.Application.Features.Identity.Commands.RequestLogout;
using Dnct.Application.Features.Identity.Queries.GenerateUserToken;
using Dnct.Application.Features.Identity.Queries.TokenRequest;
using Dnct.WebFramework.BaseController;
using Dnct.WebFramework.Swagger;
using Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1.IdentityManagement;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/User")]
public class IdentityController : BaseController
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> CreateUser(UserCreateCommand model)
    {
        var result = await _mediator.Send(model);
        return OperationResult(result);
    }


    [HttpPost("TokenRequest")]
    public async Task<IActionResult> TokenRequest(UserTokenRequestQuery model)
    {
        var query = await _mediator.Send(model);
        return OperationResult(query);
    }

    [HttpPost("LoginConfirmation")]
    public async Task<IActionResult> ValidateUser(GenerateUserTokenQuery model)
    {
        var result = await _mediator.Send(model);

        return OperationResult(result);
    }

    [HttpPost("RefreshSignIn")]
    [RequireTokenWithoutAuthorization]
    public async Task<IActionResult> RefreshUserToken(RefreshUserTokenCommand model)
    {
        var checkCurrentAccessTokenValidity =await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

        if (checkCurrentAccessTokenValidity.Succeeded)
            return BadRequest("Current access token is valid. No need to refresh");

        var newTokenResult = await _mediator.Send(model);

        return OperationResult(newTokenResult);
    }

    [HttpPost("Logout")]
    [Authorize]
    public async Task<IActionResult> RequestLogout()
    {
        var commandResult = await _mediator.Send(new RequestLogoutCommand(base.UserId));

        return OperationResult(commandResult);
    }
}