using Dnct.Application.Models.Common;
using Dnct.SharedKernel.ValidationBase.Contracts;
using Dnct.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Model;

namespace Dnct.Application.Features.Table.Commands.Create
{
    public class DeleteUserQueryCommand : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteUserQueryCommand>
    {
        public int UserQueryId { get; set; }
        public int TableConfigId { get; set; }
        
        public IValidator<DeleteUserQueryCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteUserQueryCommand> validator)
        {
            validator.RuleFor(c => c.UserQueryId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserQueryId is required");
            validator.RuleFor(c => c.TableConfigId)
                .NotEmpty()
                .NotNull()
                .WithMessage("TableConfigId is required");

            return validator;
        }
    }

    internal class CreateOrUpdateUserQueryCommandHandler : IRequestHandler<DeleteUserQueryCommand, OperationResult<bool>>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public CreateOrUpdateUserQueryCommandHandler(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }

        public async ValueTask<OperationResult<bool>> Handle(DeleteUserQueryCommand request, CancellationToken cancellationToken)
        {
            await _userQueryRepository.Delete(new UserQueryModel()
            {
                UserQueryId = request.UserQueryId,
                TableConfigId = request.TableConfigId,
            });
            
            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
