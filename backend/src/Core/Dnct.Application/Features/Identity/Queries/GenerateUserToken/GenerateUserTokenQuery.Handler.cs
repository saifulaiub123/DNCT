using Dnct.Application.Contracts;
using Dnct.Application.Contracts.Identity;
using Dnct.Application.Models.Common;
using Dnct.Application.Models.Jwt;
using Dnct.SharedKernel.Extensions;
using Mediator;

namespace Dnct.Application.Features.Identity.Queries.GenerateUserToken;

internal class GenerateUserTokenQueryHandler : IRequestHandler<GenerateUserTokenQuery, OperationResult<AuthToken>>
{
    private readonly IJwtService _jwtService;
    private readonly IAppUserManager _userManager;


    public GenerateUserTokenQueryHandler(IJwtService jwtService, IAppUserManager userManager)
    {
        _jwtService = jwtService;
        _userManager = userManager;
    }

    public async ValueTask<OperationResult<AuthToken>> Handle(GenerateUserTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserByCode(request.UserKey);

        if (user is null)
            return OperationResult<AuthToken>.FailureResult("User Not found");

        var result = user.PhoneNumberConfirmed? await _userManager.VerifyUserCode(
            user, request.Code):await _userManager.ChangePhoneNumber(user,user.PhoneNumber,request.Code);


        if (!result.Succeeded)
            return OperationResult<AuthToken>.FailureResult(result.Errors.StringifyIdentityResultErrors());

        await _userManager.UpdateUserAsync(user);

        var token = await _jwtService.GenerateAsync(user);

        return OperationResult<AuthToken>.SuccessResult(token);
    }
}