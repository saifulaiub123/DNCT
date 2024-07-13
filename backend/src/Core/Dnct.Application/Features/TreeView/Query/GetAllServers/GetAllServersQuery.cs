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
        private readonly IMapper _mapper;

        public GetAllServersQueryHandler(
            ILogger<GetAllServersQueryHandler> logger,
            IConnectionMasterRepository connectionMasterRepository, 
            IMapper mapper)
        {
            _logger = logger;
            _connectionMasterRepository = connectionMasterRepository;
            _mapper = mapper;
        }


        public async ValueTask<OperationResult<List<GetAllServerResponse>>> Handle(GetAllServersQuery request, CancellationToken cancellationGetServerInfo)
        {
            var servers = await _connectionMasterRepository.GetAllServer();
            var mappedResult = _mapper.Map<List<GetAllServerResponse>>(servers);

            return OperationResult<List<GetAllServerResponse>>.SuccessResult(mappedResult);
        }
    }
}
