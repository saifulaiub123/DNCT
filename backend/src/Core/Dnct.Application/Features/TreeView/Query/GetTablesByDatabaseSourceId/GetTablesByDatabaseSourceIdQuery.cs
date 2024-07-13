using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Features.TreeView.Query.GetDatabasesByServerId;
using Dnct.Application.Features.TreeView.Query.GetTablesByDatabaseSourceId;
using Dnct.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Dnct.Application.Features.TreeView.Query.GetTablesByDatabaseSourceIdId
{
    public class GetTablesByDatabaseSourceIdQuery : IRequest<OperationResult<List<GetTablesByDatabaseSourceIdResponse>>>
    {
        public int Id { get; set; }
    };



    public class GetTablesByDatabaseSourceIdQueryHandler : IRequestHandler<GetTablesByDatabaseSourceIdQuery, OperationResult<List<GetTablesByDatabaseSourceIdResponse>>>
    {
        private readonly ILogger<GetTablesByDatabaseSourceIdQueryHandler> _logger;
        private readonly IDatabaseSourcesRepository _databaseSourcesRepository;
        private readonly IMapper _mapper;

        public GetTablesByDatabaseSourceIdQueryHandler(
            ILogger<GetTablesByDatabaseSourceIdQueryHandler> logger,
            IDatabaseSourcesRepository databaseSourcesRepository,
            IMapper mapper)
        {
            _logger = logger;
            _databaseSourcesRepository = databaseSourcesRepository;
            _mapper = mapper;
        }


        public async ValueTask<OperationResult<List<GetTablesByDatabaseSourceIdResponse>>> Handle(GetTablesByDatabaseSourceIdQuery request, CancellationToken cancellationGetServerInfo)
        {
            var tables = await _databaseSourcesRepository.GetTablesByDatabaseSourceId(request.Id);
            var mappedResult = _mapper.Map<List<GetTablesByDatabaseSourceIdResponse>>(tables);

            return OperationResult<List<GetTablesByDatabaseSourceIdResponse>>.SuccessResult(mappedResult);
        }
    }
}

