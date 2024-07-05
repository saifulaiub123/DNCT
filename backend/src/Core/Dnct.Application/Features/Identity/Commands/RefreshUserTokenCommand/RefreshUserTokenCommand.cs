using Dnct.Application.Models.Common;
using Dnct.Application.Models.Jwt;
using Dnct.SharedKernel.ValidationBase;
using Dnct.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace Dnct.Application.Features.Identity.Commands.RefreshUserTokenCommand;

public record RefreshUserTokenCommand(Guid RefreshToken) : IRequest<OperationResult<AuthToken>>,
    IValidatableModel<RefreshUserTokenCommand>
{
    public IValidator<RefreshUserTokenCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RefreshUserTokenCommand> validator)
    {
        validator.RuleFor(c => c.RefreshToken)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter valid user refresh token");

        return validator;
    }
};