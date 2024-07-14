using AutoMapper;
using Dnct.Application.Contracts.Identity;
using Dnct.Application.Contracts;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dnct.Application.Features.Server.Query.GetServerInfo
{
    public class GetAllServersQuery : IRequest<OperationResult<List<GetAllServerResponse>>>
    {
        
    };



    public class GetAllServersQueryHandler : IRequestHandler<GetAllServersQuery, OperationResult<List<GetAllServerResponse>>>
    {
        private readonly ILogger<GetAllServersQueryHandler> _logger;
        private readonly IConnectionMasterRepository _connectionMasterRepository;
        private readonly IDatabaseSourcesRepository _databaseSourcesRepository;
        private readonly IMapper _mapper;

        public GetAllServersQueryHandler(
            ILogger<GetAllServersQueryHandler> logger,
            IConnectionMasterRepository connectionMasterRepository, 
            IMapper mapper, IDatabaseSourcesRepository databaseSourcesRepository)
        {
            _logger = logger;
            _connectionMasterRepository = connectionMasterRepository;
            _mapper = mapper;
            _databaseSourcesRepository = databaseSourcesRepository;
        }


        public async ValueTask<OperationResult<List<GetAllServerResponse>>> Handle(GetAllServersQuery request, CancellationToken cancellationGetServerInfo)
        {
            var servers = await _connectionMasterRepository.GetAllServer();
            var databases = await _databaseSourcesRepository.GetDatabasesByServerIds(servers.Select(x=> x.ContnId).ToList());
            var connectionIds = databases.Select(x => x.ServerId).Distinct().ToList();

            var mappedResult = _mapper.Map<List<GetAllServerResponse>>(servers);

            mappedResult = mappedResult.Select(x => new GetAllServerResponse()
            {
                Id = x.Id,
                Name = x.Name,
                HasChildren = connectionIds.Contains(x.Id) ? true : false,
                NodeType = "Server"

            }).ToList();

            return OperationResult<List<GetAllServerResponse>>.SuccessResult(mappedResult);
        }
    }
}
