using Dnct.Application.Contracts;
using Dnct.Application.Contracts.Identity;
using Dnct.Application.Models.Common;
using Dnct.SharedKernel.ValidationBase;
using Dnct.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Dnct.Application.Features.Identity.Queries.Token
{
    public record TokenQuery(
        string Email,
        string Password) : IRequest<OperationResult<TokenQueryResponse>>,
        IValidatableModel<TokenQuery>
    {
        public IValidator<TokenQuery> ValidateApplicationModel(
            ApplicationBaseValidationModelProvider<TokenQuery> validator)
        {
            validator.RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter your email");

            validator
                .RuleFor(c => c.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("User must have password");

            return validator;
        }
    };



    public class TokenQueryHandler : IRequestHandler<TokenQuery, OperationResult<TokenQueryResponse>>
    {
        private readonly IAppUserManager _userManager;
        private readonly IMediator _mediator;
        private readonly ILogger<TokenQueryHandler> _logger;
        private readonly IJwtService _jwtService;

        public TokenQueryHandler(
            IAppUserManager userManager, 
            IMediator mediator, 
            ILogger<TokenQueryHandler> logger, 
            IJwtService jwtService
            )
        {
            _userManager = userManager;
            _mediator = mediator;
            _logger = logger;
            _jwtService = jwtService;
        }


        public async ValueTask<OperationResult<TokenQueryResponse>> Handle(TokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetByUserName(request.Email);

            if (user is null)
                return OperationResult<TokenQueryResponse>.NotFoundResult("User Not found");

            //var code = user.PhoneNumberConfirmed ? await _userManager.GenerateOtpCode(user) : await _userManager.GeneratePhoneNumberConfirmationToken(user, user.PhoneNumber);

            //_logger.LogWarning($"Generated Code for user Id {user.Id} is {code}");

            var signInResult = await _userManager.UserLogin(user, request.Password);
            if (signInResult.Succeeded)
            {
                var token = await _jwtService.GenerateAsync(user);



                
            }

            return OperationResult<TokenQueryResponse>.SuccessResult(new TokenQueryResponse { Email = "" });
        }
    }
}
