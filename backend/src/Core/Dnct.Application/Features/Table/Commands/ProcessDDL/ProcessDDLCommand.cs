
using Dnct.Application.Models.Common;
using Dnct.SharedKernel.ValidationBase.Contracts;
using Dnct.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;
using Dnct.Application.Contracts.Identity;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Features.Order.Commands;
using Dnct.Application.Features.Table.Commands.ProcessDDL;


namespace Dnct.Application.Features.Table.Commands.DDLProcess
{
    public class ProcessDDLCommand : IRequest<OperationResult<ProcessDDLResponse>>, IValidatableModel<ProcessDDLCommand>
    {
        public string Content { get; set; }

        public IValidator<ProcessDDLCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ProcessDDLCommand> validator)
        {
            validator.RuleFor(c => c.Content)
                .NotEmpty()
                .NotNull()
                .WithMessage("DDL text must not be null or empty");

            return validator;
        }
    }

    public class ProcessDDLCommandHandler : IRequestHandler<ProcessDDLCommand, OperationResult<ProcessDDLResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessDDLCommandHandler(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<OperationResult<ProcessDDLResponse>> Handle(ProcessDDLCommand request, CancellationToken cancellationToken)
        {
            Random _random = new Random();
            var model = new ProcessDDLResponse()
            {
                Database = _random.Next(0, 1000).ToString(),
                Schema = _random.Next(0, 1000).ToString(),
                TableName = _random.Next(0, 1000).ToString(),
            };

            return OperationResult<ProcessDDLResponse>.SuccessResult(model);
        }
    }
}
