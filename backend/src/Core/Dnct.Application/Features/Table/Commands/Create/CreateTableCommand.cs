
using Dnct.Application.Features.Order.Commands;
using Dnct.Application.Models.Common;
using Dnct.SharedKernel.ValidationBase.Contracts;
using Dnct.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;
using Dnct.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Dnct.Application.Features.Table.Commands.Create
{
    public class CreateTableCommand : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateTableCommand>
    {
        public string TableName { get; set; }
        public string Connection { get; set; }
        public string Databse { get; set; }

        public IValidator<CreateTableCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateTableCommand> validator)
        {
            validator.RuleFor(c => c.TableName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Table name is required");
            validator.RuleFor(c => c.Connection)
                .NotEmpty()
                .NotNull()
                .WithMessage("Connection is required");
            validator.RuleFor(c => c.Databse)
                .NotEmpty()
                .NotNull()
                .WithMessage("Database is required");

            return validator;
        }
    }

    public async ValueTask<OperationResult<bool>> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserByIdAsync(request.UserId);

        if (user == null)
            return OperationResult<bool>.FailureResult("User Not Found");

        await _unitOfWork.OrderRepository.AddOrderAsync(new Domain.Entities.Order.Order()
        { UserId = user.Id, OrderName = request.OrderName });

        await _unitOfWork.CommitAsync();

        return OperationResult<bool>.SuccessResult(true);
    }
}
