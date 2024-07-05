using Dnct.Application.Contracts;
using Dnct.Application.Models.Common;
using Dnct.Application.Models.Jwt;
using Mediator;

namespace Dnct.Application.Features.Identity.Commands.RefreshUserTokenCommand
{
    internal class RefreshUserTokenCommandHandler : IRequestHandler<RefreshUserTokenCommand,OperationResult<AuthToken>>
    {
        private readonly IJwtService _jwtService;

        public RefreshUserTokenCommandHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async ValueTask<OperationResult<AuthToken>> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
        {
            var newToken = await _jwtService.RefreshToken(request.RefreshToken);

            if(newToken is null)
                return OperationResult<AuthToken>.FailureResult("Invalid refresh token");

            return OperationResult<AuthToken>.SuccessResult(newToken);
        }
    }
}
