using AutoMapper;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Features.RunTimeParametersMaster.Query.GetAll;
using Dnct.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Application.Features.TableInstanceRunTime.Query
{
    public class GetAllTableInstanceRunTimeQuery : IRequest<OperationResult<List<GetAllTableInstanceRunTimeResponse>>>
    {
        public int TableConfigId { get; set; }

    };


    public class GetAllTableInstanceRunTimeQueryHandler : IRequestHandler<GetAllTableInstanceRunTimeQuery, OperationResult<List<GetAllTableInstanceRunTimeResponse>>>
    {
        private readonly ILogger<GetAllTableInstanceRunTimeQueryHandler> _logger;
        private readonly ITableInstanceRunTimeRepository _tableInstanceRunTimeRepository;
        private readonly IMapper _mapper;

        public GetAllTableInstanceRunTimeQueryHandler(
            ILogger<GetAllTableInstanceRunTimeQueryHandler> logger,
            IMapper mapper,
            ITableInstanceRunTimeRepository tableInstanceRunTimeRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _tableInstanceRunTimeRepository = tableInstanceRunTimeRepository;
        }


        public async ValueTask<OperationResult<List<GetAllTableInstanceRunTimeResponse>>> Handle(GetAllTableInstanceRunTimeQuery request, CancellationToken cancellationGetServerInfo)
        {
            var data = (await _tableInstanceRunTimeRepository.GetAll(request.TableConfigId)).ToList();

            var mappedResult = _mapper.Map<List<GetAllTableInstanceRunTimeResponse>>(data);

            return OperationResult<List<GetAllTableInstanceRunTimeResponse>>.SuccessResult(mappedResult);
        }
    }
}
