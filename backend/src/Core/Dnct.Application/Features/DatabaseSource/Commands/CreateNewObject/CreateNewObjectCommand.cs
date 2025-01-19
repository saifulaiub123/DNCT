
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
        public int DatabaseSourceId { get; set; }
        public string ConnectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectKind { get; set; }
        public string SqlToUse { get; set; }
        public string DeltaRowsIdentificationLogic { get; set; }
        public string QueryBand { get; set; }
        public char? TruncateBeforeLoad { get; set; }
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
        private readonly ICurrentUserService _currentUser;
        private readonly IDatabaseSourcesRepository _databaseSourcesRepository;

        public CreateNewObjectCommandHandler(
            IUnitOfWork unitOfWork,
            IAppUserManager userManager,
            IDatabaseSourcesRepository databaseSourcesRepository
,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _databaseSourcesRepository = databaseSourcesRepository;
            _currentUser = currentUser;
        }

        public async ValueTask<OperationResult<bool>> Handle(CreateNewObjectCommand command, CancellationToken cancellationToken)
        {
            await _databaseSourcesRepository.Update(new DatabaseSources()
            {
                DatbsSrcId = command.DatabaseSourceId,
                Usrname = _currentUser.Username,
                RepstryName = command.ConnectionName,
                ConctnName = command.ConnectionName,
                TblDbsName = command.DatabaseName,
                TblName = command.ObjectName,
                TablKind = command.ObjectKind,
                SqlToUse = command.SqlToUse,
                Queryband = command.QueryBand,
                AdtnlWherCondtns = command.AdditionalWhereCondition,
                TrunctTblAftrLoad = command.TruncateBeforeLoad,
                //SetlSetupName = command.SetlSetupName,
                ObjctAls = command.Alias,
                DedupByColmns = command.DedupLogic,
                DeltRowIdntfctn = command.DeltaRowsIdentificationLogic,
                EstmtdTblSiz = command.EstimatedTableSize,
                Type2Colmns = string.Empty,
                ObjctNatr = command.ObjectNature,
                TargetObjcConName = command.TargetDatabaseConnection,
                PartitionClause = command.PartitioningClause,
                PartitionColmns = string.Empty,
                DedupLogic = command.DedupLogic,
                PkColmns = string.Empty,
                YearsOfHistory = command.MonthsOfHistory,
                ConfgrtnEffStartTs = DateTime.UtcNow,
                ConfgrtnEffEndTs = new DateTime(9999, 1, 1, 1, 1, 1, DateTimeKind.Utc),
            });

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
