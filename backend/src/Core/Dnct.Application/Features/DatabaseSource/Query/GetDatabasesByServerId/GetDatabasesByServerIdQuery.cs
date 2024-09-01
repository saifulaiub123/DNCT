using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Dnct.Application.Features.TreeView.Query.GetDatabasesByServerId
{
    public class GetDatabasesByServerIdQuery : IRequest<OperationResult<List<GetDatabasesByServerIdResponse>>>
    {
        public int Id { get; set; }
    };



    public class GetDatabasesByServerIdQueryHandler : IRequestHandler<GetDatabasesByServerIdQuery, OperationResult<List<GetDatabasesByServerIdResponse>>>
    {
        private readonly ILogger<GetDatabasesByServerIdQueryHandler> _logger;
        private readonly IDatabaseSourcesRepository _databaseSourcesRepository;
        private readonly IMapper _mapper;


        public GetDatabasesByServerIdQueryHandler(
            ILogger<GetDatabasesByServerIdQueryHandler> logger,
            IDatabaseSourcesRepository databaseSourcesRepository,
            IMapper mapper)
        {
            _logger = logger;
            _databaseSourcesRepository = databaseSourcesRepository;
            _mapper = mapper;
        }


        public async ValueTask<OperationResult<List<GetDatabasesByServerIdResponse>>> Handle(GetDatabasesByServerIdQuery request, CancellationToken cancellationGetServerInfo)
        {
            var databases = await _databaseSourcesRepository.GetDatabasesByServerId(request.Id);
            var mappedResult = _mapper.Map<List<GetDatabasesByServerIdResponse>>(databases);

            return OperationResult<List<GetDatabasesByServerIdResponse>>.SuccessResult(mappedResult);
        }
    }
}

