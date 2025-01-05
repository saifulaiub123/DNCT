using Dnct.Application.Models.Common;
using Dnct.SharedKernel.ValidationBase.Contracts;
using Dnct.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Model;

namespace Dnct.Application.Features.Table.Commands.DeleteUserQueryCommand
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

    internal class DeleteUserQueryCommandHandler : IRequestHandler<DeleteUserQueryCommand, OperationResult<bool>>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public DeleteUserQueryCommandHandler(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }

        public async ValueTask<OperationResult<bool>> Handle(DeleteUserQueryCommand request, CancellationToken cancellationToken)
        {
            if (request.UserQueryId == -1)
            {
                await _userQueryRepository.Create(new UserQueryModel()
                {
                    UserQueryId = request.UserQueryId,
                    TableConfigId = request.TableConfigId,
                    UserQuery = request.UserQuery,
                    BaseQueryIndicator = request.BaseQueryIndicator,
                    QueryOrderIndicator = request.QueryOrderIndicator,
                    RowInsertTimestamp = request.RowInsertTimestamp,
                });
            }
            else
            {
                var userQueries = (await _userQueryRepository.GetUserQueryByQueryId(request.UserQueryId));
                await _userQueryRepository.Update(new UserQueryModel()
                {
                    UserQueryId = userQueries.UserQueryId,
                    TableConfigId = userQueries.TableConfigId,
                    UserQuery = request.UserQuery,
                    BaseQueryIndicator = request.BaseQueryIndicator,
                    QueryOrderIndicator = request.QueryOrderIndicator,
                    RowInsertTimestamp = DateTime.Now,
                });
            }

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
