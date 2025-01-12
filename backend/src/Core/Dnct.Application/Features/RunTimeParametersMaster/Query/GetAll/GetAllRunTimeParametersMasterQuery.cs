using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Application.Features.RunTimeParametersMaster.Query.GetAll
{
    public class GetAllRunTimeParametersMasterQuery : IRequest<OperationResult<List<GetAllRunTimeParametersMasterResponse>>>
    {
        public int TableConfigId { get; set; }

    };


    public class GetAllRunTimeParametersMasterQueryHandler : IRequestHandler<GetAllRunTimeParametersMasterQuery, OperationResult<List<GetAllRunTimeParametersMasterResponse>>>
    {
        private readonly ILogger<GetAllRunTimeParametersMasterQueryHandler> _logger;
        private readonly IRunTimeParametersMasterRepository _runTimeParametersMasterRepository;
        private readonly IMapper _mapper;

        public GetAllRunTimeParametersMasterQueryHandler(
            ILogger<GetAllRunTimeParametersMasterQueryHandler> logger,
            IMapper mapper,
            IRunTimeParametersMasterRepository runTimeParametersMasterRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _runTimeParametersMasterRepository = runTimeParametersMasterRepository;
        }


        public async ValueTask<OperationResult<List<GetAllRunTimeParametersMasterResponse>>> Handle(GetAllRunTimeParametersMasterQuery request, CancellationToken cancellationGetServerInfo)
        {
            var data = (await _runTimeParametersMasterRepository.GetAll(request.TableConfigId)).ToList();

            var mappedResult = _mapper.Map<List<GetAllRunTimeParametersMasterResponse>>(data);

            return OperationResult<List<GetAllRunTimeParametersMasterResponse>>.SuccessResult(mappedResult);
        }
    }
}
