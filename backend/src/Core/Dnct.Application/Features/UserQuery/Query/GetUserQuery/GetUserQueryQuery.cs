using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Dnct.Application.Features.UserQuery.Query.GetUserQuery
{
    public class GetUserQueryQuery : IRequest<OperationResult<List<GetUserQueryResponse>>>
    {

    };

    public class GetUserQueryQueryHandler : IRequestHandler<GetUserQueryQuery, OperationResult<List<GetUserQueryResponse>>>
    {
        private readonly ILogger<GetUserQueryQueryHandler> _logger;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;

        public GetUserQueryQueryHandler(
            ILogger<GetUserQueryQueryHandler> logger,
            IMapper mapper,
            IUserQueryRepository userQueryRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userQueryRepository = userQueryRepository;
        }


        public async ValueTask<OperationResult<List<GetUserQueryResponse>>> Handle(GetUserQueryQuery request, CancellationToken cancellationGetServerInfo)
        {
            var result = new List<GetUserQueryResponse>();

            var userQueries = (await _userQueryRepository.GetUserQuries()).ToList();

            var mappedResult = _mapper.Map<List<GetUserQueryResponse>>(userQueries);

            return OperationResult<List<GetUserQueryResponse>>.SuccessResult(mappedResult);
        }
    }
}
