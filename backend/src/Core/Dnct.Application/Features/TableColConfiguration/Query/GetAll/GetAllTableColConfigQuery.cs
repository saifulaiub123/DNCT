using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Dnct.Application.Features.UserQuery.Query.GetAllTableColConfig
{
    public class GetAllTableColConfigQuery : IRequest<OperationResult<List<GetAllTableColConfigResponse>>>
    {

    };


    public class GetAllTableColConfigQueryHandler : IRequestHandler<GetAllTableColConfigQuery, OperationResult<List<GetAllTableColConfigResponse>>>
    {
        private readonly ILogger<GetAllTableColConfigQueryHandler> _logger;
        private readonly ITableColConfigurationRepository _tableColConfigurationRepository;
        private readonly IMapper _mapper;

        public GetAllTableColConfigQueryHandler(
            ILogger<GetAllTableColConfigQueryHandler> logger,
            IMapper mapper,
            ITableColConfigurationRepository tableColConfigurationRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _tableColConfigurationRepository = tableColConfigurationRepository;
        }


        public async ValueTask<OperationResult<List<GetAllTableColConfigResponse>>> Handle(GetAllTableColConfigQuery request, CancellationToken cancellationGetServerInfo)
        {
            var result = new List<GetAllTableColConfigResponse>();

            var data = (await _tableColConfigurationRepository.GetAll()).ToList();

            var mappedResult = _mapper.Map<List<GetAllTableColConfigResponse>>(data);

            return OperationResult<List<GetAllTableColConfigResponse>>.SuccessResult(mappedResult);
        }
    }
}
