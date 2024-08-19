
using Dnct.Application.Features.Order.Commands;
using Dnct.Application.Models.Common;
using Dnct.SharedKernel.ValidationBase.Contracts;
using Dnct.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;
using Dnct.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Identity;
using Dnct.Application.Contracts.Identity;
using Dnct.Domain.Entities;
using System.Data;

namespace Dnct.Application.Features.Table.Commands.Create
{
    public class CreateTableCommand : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateTableCommand>
    {
        public string TableName { get; set; }
        public string Connection { get; set; }
        public string DatabaseSourceId{ get; set; }

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
            validator.RuleFor(c => c.DatabaseSourceId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Database is required");

            return validator;
        }
    }

    internal class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserManager _userManager;
        private readonly IDatabaseSourcesRepository _databaseSourcesRepository;

        public CreateTableCommandHandler(
            IUnitOfWork unitOfWork, 
            IAppUserManager userManager,
            IDatabaseSourcesRepository databaseSourcesRepository
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _databaseSourcesRepository = databaseSourcesRepository;
        }

        public async ValueTask<OperationResult<bool>> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            var tableInstance = await _databaseSourcesRepository.GetTableInstance("PGRepo", "sbc_db", request.TableName);
            if(tableInstance.Any())
            {
                return OperationResult<bool>.FailureResult("Table name already exist", false);
            }
            await _databaseSourcesRepository.CrateTable(new DatabaseSources()
            {
                 RepstryName = "PGRepo",
                 ConctnName = "PGRepo",
                 TblDbsName = "sbc_db",
                 TblName = request.TableName,
                 ConfgrtnEffEndTs = DateTime.Now.AddDays(365)
            });

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
