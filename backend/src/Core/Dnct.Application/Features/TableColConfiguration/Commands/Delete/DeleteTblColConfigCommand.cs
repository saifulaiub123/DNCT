using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Features.TableColConfiguration.Commands.CreateMultiple;
using Dnct.Application.Models.Common;
using Mediator;

namespace Dnct.Application.Features.TableColConfiguration.Commands.Delete
{
    public class DeleteTblColConfigCommand : IRequest<OperationResult<bool>>
    {
        public int tbleColConfigId { get; set; }
        public int tbleConfigId { get; set; }

    }

    internal class DeleteTblColConfigCommandHandler : IRequestHandler<DeleteTblColConfigCommand, OperationResult<bool>>
    {
        private readonly ITableColConfigurationRepository _tableColConfigurationRepository;
        private readonly IMapper _mapper;

        public DeleteTblColConfigCommandHandler(IMapper mapper, ITableColConfigurationRepository tableColConfigurationRepository)
        {
            _mapper = mapper;
            _tableColConfigurationRepository = tableColConfigurationRepository;
        }

        public async ValueTask<OperationResult<bool>> Handle(DeleteTblColConfigCommand command, CancellationToken cancellationToken)
        {
            await _tableColConfigurationRepository.Delete(command.tbleColConfigId, command.tbleConfigId);

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
