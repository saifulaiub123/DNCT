using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Features.RunTimeParametersMaster.Commands.Create;
using Dnct.Application.Models.Common;
using Dnct.Domain.Model;
using Mediator;
using System.Text.Json.Serialization;

namespace Dnct.Application.Features.TableInstanceRunTime.Commands.CreateTableInstanceRunTime
{
    public class CreateTableInstanceRunTimeCommand : IRequest<OperationResult<bool>>
    {
        public List<Datar> Data { get; set; }
    }

    public class Datar
    {
        public int? TableConfigId { get; set; }
        public int? RelatedTableConfigId { get; set; }
        public int OverrideInd { get; set; }
        public string InstanceName { get; set; }
        public int InstanceOrder { get; set; }
        [JsonIgnore]
        public DateTime? ConfigurationEffectiveStartTimestamp { get; set; }
        [JsonIgnore]
        public DateTime ConfigurationEffectiveEndTimestamp { get; set; }
    }
    public class CreateTableInstanceRunTimeCommandHandler : IRequestHandler<CreateTableInstanceRunTimeCommand, OperationResult<bool>>
    {
        private readonly ITableInstanceRunTimeRepository _tableInstanceRunTimeRepository;
        private readonly IMapper _mapper;

        public CreateTableInstanceRunTimeCommandHandler(
            IMapper mapper,
            ITableInstanceRunTimeRepository tableInstanceRunTimeRepository)
        {
            _mapper = mapper;
            _tableInstanceRunTimeRepository = tableInstanceRunTimeRepository;
        }

        public async ValueTask<OperationResult<bool>> Handle(CreateTableInstanceRunTimeCommand command, CancellationToken cancellationToken)
        {

            if(!command.Data.Any()) return OperationResult<bool>.SuccessResult(true);

            await _tableInstanceRunTimeRepository.Create(command.Data.Select(x => new TableInstanceRunTimeModel()
            {
                TableConfigId = x.TableConfigId,
                RelatedTableConfigId = x.RelatedTableConfigId,
                OverrideInd = x.OverrideInd,
                ConfigurationEffectiveStartTimestamp = DateTime.Now,
                ConfigurationEffectiveEndTimestamp = new DateTime(9999, 1, 1, 1, 1, 1, DateTimeKind.Utc),
            }).ToList());

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}