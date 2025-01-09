using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Mediator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnct.Application.Models.Common;

namespace Dnct.Application.Features.LoadStragegy.Query.GetAllLoadStrategy
{
    public class GetAllLoadStrategyQuery : IRequest<OperationResult<List<GetAllLoadStrategyResponse>>>
    {

    };


    public class GetAllLoadStrategyQueryHandler : IRequestHandler<GetAllLoadStrategyQuery, OperationResult<List<GetAllLoadStrategyResponse>>>
    {
        private readonly ILogger<GetAllLoadStrategyQueryHandler> _logger;
        private readonly ILoadStrategyRepository _loadStrategyRepository;
        private readonly IMapper _mapper;

        public GetAllLoadStrategyQueryHandler(
            ILogger<GetAllLoadStrategyQueryHandler> logger,
            IMapper mapper,
            ILoadStrategyRepository loadStrategyRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _loadStrategyRepository = loadStrategyRepository;
            
        }


        public async ValueTask<OperationResult<List<GetAllLoadStrategyResponse>>> Handle(GetAllLoadStrategyQuery request, CancellationToken cancellationGetServerInfo)
        {
            var data = (await _loadStrategyRepository.GetAll()).ToList();

            var mappedResult = _mapper.Map<List<GetAllLoadStrategyResponse>>(data);

            return OperationResult<List<GetAllLoadStrategyResponse>>.SuccessResult(mappedResult);
        }
    }
}
