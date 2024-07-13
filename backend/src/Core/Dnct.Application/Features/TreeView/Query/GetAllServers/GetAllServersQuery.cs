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
    public class GetAllServersQuery : IRequest<OperationResult<GetAllServersResponse>>
    {
        
    };



    public class GetAllServersQueryHandler : IRequestHandler<GetAllServersQuery, OperationResult<GetAllServersResponse>>
    {
        private readonly ILogger<GetAllServersQueryHandler> _logger;
        private readonly IDatabaseSourcesRepository _databaseSourcesRepository;

        public GetAllServersQueryHandler(
            ILogger<GetAllServersQueryHandler> logger,
            IDatabaseSourcesRepository databaseSourcesRepository)
        {
            _logger = logger;
            _databaseSourcesRepository = databaseSourcesRepository;
        }


        public async ValueTask<OperationResult<GetAllServersResponse>> Handle(GetAllServersQuery request, CancellationToken cancellationGetServerInfo)
        {
            var servers = await _databaseSourcesRepository.GetAllServer();
            return OperationResult<GetAllServersResponse>.SuccessResult(new GetAllServersResponse());
        }
    }
}
