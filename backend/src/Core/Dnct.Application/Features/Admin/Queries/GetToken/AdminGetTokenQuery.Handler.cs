using Dnct.Application.Contracts;
using Dnct.Application.Contracts.Identity;
using Dnct.Application.Models.Common;
using Dnct.Application.Models.Jwt;
using Mediator;

namespace Dnct.Application.Features.Admin.Queries.GetToken;

public class AdminGetTokenQueryHandler:IRequestHandler<AdminGetTokenQuery,OperationResult<AuthToken>>
{
    private readonly IAppUserManager _userManager;
    private readonly IJwtService _jwtService;
    public AdminGetTokenQueryHandler(IAppUserManager userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async ValueTask<OperationResult<AuthToken>> Handle(AdminGetTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByUserName(request.UserName);

        if(user is null)
            return OperationResult<AuthToken>.FailureResult("User not found");

        var isUserLockedOut = await _userManager.IsUserLockedOutAsync(user);

        if(isUserLockedOut)
            if (user.LockoutEnd != null)
                return OperationResult<AuthToken>.FailureResult(
                    $"User is locked out. Try in {(user.LockoutEnd-DateTimeOffset.Now).Value.Minutes} Minutes");

        var passwordValidator = await _userManager.UserLogin(user, request.Password);


        if (!passwordValidator.Succeeded)
        {
          var lockoutIncrementResult= await _userManager.IncrementAccessFailedCountAsync(user);

            return OperationResult<AuthToken>.FailureResult("Password is not correct");
        }

        var token= await _jwtService.GenerateAsync(user);


        return OperationResult<AuthToken>.SuccessResult(token);
    }
}