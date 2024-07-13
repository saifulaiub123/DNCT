using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Dnct.Application.Features.TreeView.Query.GetTableInstanceByDatabaseSourceId
{
    public class GetTableInstanceByDatabaseSourceIdQuery : IRequest<OperationResult<List<GetTableInstanceByDatabaseSourceIdResponse>>>
    {
        public int Id { get; set; }
    };



    public class GetTableInstanceByDatabaseSourceIdQueryHandler : IRequestHandler<GetTableInstanceByDatabaseSourceIdQuery, OperationResult<List<GetTableInstanceByDatabaseSourceIdResponse>>>
    {
        private readonly ILogger<GetTableInstanceByDatabaseSourceIdQueryHandler> _logger;
        private readonly ITableConfigurationRepository _tableConfigurationRepository;
        private readonly IMapper _mapper;

        public GetTableInstanceByDatabaseSourceIdQueryHandler(
            ILogger<GetTableInstanceByDatabaseSourceIdQueryHandler> logger,
            ITableConfigurationRepository tableConfigurationRepository,
            IMapper mapper)
        {
            _logger = logger;
            _tableConfigurationRepository = tableConfigurationRepository;
            _mapper = mapper;
        }


        public async ValueTask<OperationResult<List<GetTableInstanceByDatabaseSourceIdResponse>>> Handle(GetTableInstanceByDatabaseSourceIdQuery request, CancellationToken cancellationGetServerInfo)
        {
            var instances = await _tableConfigurationRepository.GetTableInstanceByDatabaseSourceId(request.Id);
            var mappedResult = _mapper.Map<List<GetTableInstanceByDatabaseSourceIdResponse>>(instances);

            return OperationResult<List<GetTableInstanceByDatabaseSourceIdResponse>>.SuccessResult(mappedResult);
        }
    }
}
