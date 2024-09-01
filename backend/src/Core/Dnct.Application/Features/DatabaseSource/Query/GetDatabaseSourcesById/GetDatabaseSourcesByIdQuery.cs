using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Dnct.Application.Features.DatabaseSource.Query.GetDatabaseSourcesById
{
    public class GetDatabaseSourcesByIdQuery : IRequest<OperationResult<List<GetDatabaseSourcesByIdResponse>>>
    {
        public int Id { get; set; }
    };



    public class GetDatabaseSourcesByIdQueryHandler : IRequestHandler<GetDatabaseSourcesByIdQuery, OperationResult<List<GetDatabaseSourcesByIdResponse>>>
    {
        private readonly ILogger<GetDatabaseSourcesByIdQueryHandler> _logger;
        private readonly IDatabaseSourcesRepository _databaseSourcesRepository;
        private readonly IMapper _mapper;

        public GetDatabaseSourcesByIdQueryHandler(
            ILogger<GetDatabaseSourcesByIdQueryHandler> logger,
            IDatabaseSourcesRepository databaseSourcesRepository,
            IMapper mapper)
        {
            _logger = logger;
            _databaseSourcesRepository = databaseSourcesRepository;
            _mapper = mapper;
        }


        public async ValueTask<OperationResult<List<GetDatabaseSourcesByIdResponse>>> Handle(GetDatabaseSourcesByIdQuery request, CancellationToken cancellationGetServerInfo)
        {
            var tables = await _databaseSourcesRepository.GetDatabaseSourceById(request.Id);
            var mappedResult = _mapper.Map<List<GetDatabaseSourcesByIdResponse>>(tables);

            return OperationResult<List<GetDatabaseSourcesByIdResponse>>.SuccessResult(mappedResult);
        }
    }
}


