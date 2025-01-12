using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Dnct.Domain.Model;
using Mediator;


namespace Dnct.Application.Features.RunTimeParametersMaster.Commands.Create
{
    public class CreateRunTimeParametersMasterCommand : IRequest<OperationResult<bool>>
    {
        public List<Datat> Data { get; set; }
    }

    public class Datat
    {
        public int RuntimeParametersMasterId { get; set; }
        public string ParameterValue { get; set; }
        public int TableConfigId { get; set; }
    }
    public class  CreateRunTimeParametersMasterCommandHandler : IRequestHandler<CreateRunTimeParametersMasterCommand, OperationResult<bool>>
    {
        private readonly IRunTimeParametersMasterRepository _runTimeParametersMasterRepository;
        private readonly IMapper _mapper;

        public  CreateRunTimeParametersMasterCommandHandler(
            IMapper mapper, 
            IRunTimeParametersMasterRepository runTimeParametersMasterRepository)
        {
            _mapper = mapper;
            _runTimeParametersMasterRepository = runTimeParametersMasterRepository;
        }

        public async ValueTask<OperationResult<bool>> Handle(CreateRunTimeParametersMasterCommand command, CancellationToken cancellationToken)
        {
            await _runTimeParametersMasterRepository.Create(command.Data.Select(x=> new RunTimeParametersMasterModel()
            {
                TableConfigId =x.TableConfigId,
                ParameterValue = x.ParameterValue,
                RuntimeParametersMasterId = x.RuntimeParametersMasterId,
                ConfigurationEffectiveStartTimestamp = DateTime.Now,
                ConfigurationEffectiveEndTimestamp = new DateTime(9999, 1, 1, 1, 1, 1, DateTimeKind.Utc),
            }).ToList());
            
            return OperationResult<bool>.SuccessResult(true);
        }
    }
}