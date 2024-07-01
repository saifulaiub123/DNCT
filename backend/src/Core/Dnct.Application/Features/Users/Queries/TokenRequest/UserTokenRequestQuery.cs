using Dnct.Application.Models.Common;
using Dnct.SharedKernel.ValidationBase;
using Dnct.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.RegularExpressions;

namespace Dnct.Application.Features.Users.Queries.TokenRequest;

public record UserTokenRequestQuery(string UserPhoneNumber) : IRequest<OperationResult<UserTokenRequestQueryResponse>>,
    IValidatableModel<UserTokenRequestQuery>
{
    public IValidator<UserTokenRequestQuery> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UserTokenRequestQuery> validator)
    {

        validator.RuleFor(c => c.UserPhoneNumber).NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")).WithMessage("Phone number is not valid");


        return validator;
    }
};