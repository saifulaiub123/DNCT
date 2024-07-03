using AutoMapper;
using Dnct.Application.Contracts.Identity;
using Dnct.Application.Models.Common;
using Dnct.Application.Profiles;
using Dnct.Domain.Entities.User;
using Dnct.SharedKernel.ValidationBase;
using Dnct.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Dnct.Application.Features.Identity.Commands.Create;

public record UserCreateCommand
    (
        string Name, 
        string Email,
        string Password
    ) : IRequest<OperationResult<UserCreateCommandResult>>,IValidatableModel<UserCreateCommand>,ICreateMapper<User>
{

    public IValidator<UserCreateCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UserCreateCommand> validator)
    {

        validator
            .RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("User must have name");

        validator.RuleFor(c => c.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter your email");

        validator
            .RuleFor(c => c.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("User must have password");


        //validator.RuleFor(c => c.PhoneNumber).NotEmpty()
        //    .NotNull().WithMessage("Phone Number is required.")
        //    .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
        //    .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
        //    .Matches(new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")).WithMessage("Phone number is not valid");

        return validator;
    }


    internal class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, OperationResult<UserCreateCommandResult>>
    {

        private readonly IAppUserManager _userManager;
        private readonly ILogger<UserCreateCommandHandler> _logger;
        private readonly IMapper _mapper;
        public UserCreateCommandHandler(IAppUserManager userRepository, ILogger<UserCreateCommandHandler> logger, IMapper mapper)
        {
            _userManager = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async ValueTask<OperationResult<UserCreateCommandResult>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _userManager.IsExistUserName(request.Email);

            if (userExist)
                return OperationResult<UserCreateCommandResult>.FailureResult("Email already exists");

            var user = _mapper.Map<User>(request);

            user.UserName = request.Email;
            user.PhoneNumberConfirmed = true;
            user.EmailConfirmed = true;

            var createResult = await _userManager.CreateUser(user,request.Password);

            if (!createResult.Succeeded)
            {
                return OperationResult<UserCreateCommandResult>.FailureResult(string.Join(",", createResult.Errors.Select(c => c.Description)));
            }

            var code = await _userManager.GeneratePhoneNumberConfirmationToken(user, user.PhoneNumber);


            _logger.LogWarning($"Generated Code for User ID {user.Id} is {code}");

            //TODO Send Code Via Sms Provider

            return OperationResult<UserCreateCommandResult>.SuccessResult(new UserCreateCommandResult { UserGeneratedKey = user.GeneratedCode });
        }
    }
}