
using Dnct.Application.Contracts.Identity;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Dnct.Domain.Entities;
using Dnct.SharedKernel.ValidationBase.Contracts;
using Dnct.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;

namespace Dnct.Application.Features.DatabaseSource.Commands.CreateNewObject
{
    public class CreateNewObjectCommand : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateNewObjectCommand>
    {
        public string DatabaseSourceId { get; set; }
        public string ConnectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectKind { get; set; }
        public string SqlToUse { get; set; }
        public string DeltaRowsIdentificationLogic { get; set; }
        public string QueryBand { get; set; }
        public string TruncateBeforeLoad { get; set; }
        public string EtlFramework { get; set; }
        public string DedupLogic { get; set; }
        public int EstimatedTableSize { get; set; }
        public string Alias { get; set; }
        public string AdditionalWhereCondition { get; set; }
        public string ObjectNature { get; set; }
        public string PartitioningClause { get; set; }
        public int MonthsOfHistory { get; set; }
        public string TargetDatabaseConnection { get; set; }

        public IValidator<CreateNewObjectCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateNewObjectCommand> validator)
        {
            validator.RuleFor(c => c.DatabaseSourceId)
                .NotEmpty()
                .NotNull()
                .WithMessage("DatabaseSourceId is required");
            validator.RuleFor(c => c.ConnectionName)
                .NotEmpty()
                .NotNull()
                .WithMessage("ConnectionName is required");
            validator.RuleFor(c => c.DatabaseName)
                .NotEmpty()
                .NotNull()
                .WithMessage("DatDatabaseName is required");

            return validator;
        }
    }

    internal class CreateNewObjectCommandHandler : IRequestHandler<CreateNewObjectCommand, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserManager _userManager;
        private readonly IDatabaseSourcesRepository _databaseSourcesRepository;

        public CreateNewObjectCommandHandler(
            IUnitOfWork unitOfWork,
            IAppUserManager userManager,
            IDatabaseSourcesRepository databaseSourcesRepository
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _databaseSourcesRepository = databaseSourcesRepository;
        }

        public async ValueTask<OperationResult<bool>> Handle(CreateNewObjectCommand request, CancellationToken cancellationToken)
        {
            //var tableInstance = await _databaseSourcesRepository.GetTableInstance("PGRepo", "sbc_db", request.TableName);
            //if (tableInstance.Any())
            //{
            //    return OperationResult<bool>.FailureResult("Table name already exist", false);
            //}
            //await _databaseSourcesRepository.CrateTable(new DatabaseSources()
            //{
            //    RepstryName = "PGRepo",
            //    ConctnName = "PGRepo",
            //    TblDbsName = "sbc_db",
            //    TblName = request.TableName,
            //    ConfgrtnEffEndTs = DateTime.Now.AddDays(365)
            //});

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
