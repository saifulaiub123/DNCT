using AutoMapper;
using Dnct.Application.Contracts.Identity;
using Dnct.Application.Models.Common;
using Dnct.Application.Profiles;
using Dnct.Domain.Entities.User;
using Dnct.SharedKernel.Extensions;
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
    ) : IRequest<OperationResult<bool>>,IValidatableModel<UserCreateCommand>,ICreateMapper<User>
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


    internal class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, OperationResult<bool>>
    {

        private readonly IAppUserManager _userManager;
        private readonly ILogger<UserCreateCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRoleManagerService _roleManagerService;

        public UserCreateCommandHandler(
            IAppUserManager userRepository, 
            ILogger<UserCreateCommandHandler> logger, 
            IMapper mapper, 
            IRoleManagerService roleManagerService
            )
        {
            _userManager = userRepository;
            _logger = logger;
            _mapper = mapper;
            _roleManagerService = roleManagerService;
        }

        public async ValueTask<OperationResult<bool>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManagerService.GetRoleByNameAsync("Admin");

            if (role is null)
                return OperationResult<bool>.NotFoundResult("Role admin not found");

            var userExist = await _userManager.IsExistUserName(request.Email);

            if (userExist)
                return OperationResult<bool>.FailureResult("Email already exists");

            var newAdmin = new User { UserName = request.Email, Email = request.Email, Name= request.Name };

            var adminCreateResult =
                await _userManager.CreateUserWithPasswordAsync(
                    newAdmin, request.Password);

            if (!adminCreateResult.Succeeded)
                return OperationResult<bool>.FailureResult(adminCreateResult.Errors.StringifyIdentityResultErrors());

            var addAdminToRoleResult = await _userManager.AddUserToRoleAsync(newAdmin, role);

            if (addAdminToRoleResult.Succeeded)
                return OperationResult<bool>.SuccessResult(true);

            return OperationResult<bool>.FailureResult(addAdminToRoleResult.Errors.StringifyIdentityResultErrors());
        }
    }
}