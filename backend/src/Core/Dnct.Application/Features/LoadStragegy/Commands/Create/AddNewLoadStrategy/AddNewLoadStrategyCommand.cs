using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Dnct.Domain.Model;
using Mediator;

namespace Dnct.Application.Features.LoadStragegy.Commands.Create.AddNewLoadStrategy
{
    public class AddNewLoadStrategyCommand : IRequest<OperationResult<bool>>
    {
        public int LoadStrategyId { get; set; }
        public int TblConfigId { get; set; }

    }

    public class AddNewLoadStrategyCommandHandler : IRequestHandler<AddNewLoadStrategyCommand, OperationResult<bool>>
    {
        private readonly ILoadStrategyRepository _loadStrategyRepository;
        private readonly IMapper _mapper;

        public AddNewLoadStrategyCommandHandler(IMapper mapper, ILoadStrategyRepository loadStrategyRepository)
        {
            _mapper = mapper;
            _loadStrategyRepository = loadStrategyRepository;
        }

        public async ValueTask<OperationResult<bool>> Handle(AddNewLoadStrategyCommand command, CancellationToken cancellationToken)
        {
            var loadStrategy = await _loadStrategyRepository.Get(command.TblConfigId, command.LoadStrategyId);
            if (loadStrategy is null)
            {
                var item = new TblLoadStrategyModel()
                {
                    TableConfigId = command.TblConfigId,
                    LoadStrategyId = command.LoadStrategyId,
                    ConfigurationEffectiveStartTimestamp = DateTime.Now,
                    ConfigurationEffectiveEndTimestamp = new DateTime(9999, 1, 1, 1, 1, 1, DateTimeKind.Utc),

                };
                await _loadStrategyRepository.Create(item);
            }

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}