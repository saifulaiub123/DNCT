using Dnct.Application.Models.Common;
using Dnct.SharedKernel.ValidationBase.Contracts;
using Dnct.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;
using Dnct.Application.Contracts.Persistence;
using Dnct.Domain.Model;
using AutoMapper;

namespace Dnct.Application.Features.TableColConfiguration.Commands.CreateMultiple
{
    public class CreateMultipleTblColConfigCommand : IRequest<OperationResult<bool>>
    {
        public List<TableColConfigurationModel> Data { get; set; }
        
    }

    internal class CreateMultipleTblColConfigCommandHandler : IRequestHandler<CreateMultipleTblColConfigCommand, OperationResult<bool>>
    {
        private readonly ITableColConfigurationRepository _tableColConfigurationRepository;
        private readonly IMapper _mapper;

        public CreateMultipleTblColConfigCommandHandler(IMapper mapper, ITableColConfigurationRepository tableColConfigurationRepository)
        {
            _mapper = mapper;
            _tableColConfigurationRepository = tableColConfigurationRepository;
        }

        public async ValueTask<OperationResult<bool>> Handle(CreateMultipleTblColConfigCommand command, CancellationToken cancellationToken)
        {

            foreach (var item in command.Data)
            {
                if (item.TblColConfgrtnId == -1)
                {
                    await _tableColConfigurationRepository.Create(
                        _mapper.Map<Domain.Entities.TableColConfiguration>(item));
                }
                else
                {
                    var data = await _tableColConfigurationRepository.GetById(item.TblColConfgrtnId, item.TblConfgrtnId);
                    if (data is not null)
                    {
                        data.ColmnName = item.ColmnName;
                        data.DataType = item.DataType;
                        data.ColmnTrnsfrmtnStep1 = item.ColmnTrnsfrmtnStep1;
                        data.GenrtIdInd = item.GenrtIdInd;
                        data.IdGenrtnStratgyId = item.IdGenrtnStratgyId;
                        data.Type2StartInd = item.Type2StartInd;
                        data.Type2EndInd = item.Type2EndInd;
                        data.CurrRowInd = item.CurrRowInd;
                        data.Pattern1 = item.Pattern1;
                        data.Pattern2 = item.Pattern2;
                        data.Pattern3 = item.Pattern3;
                        data.LadInd = item.LadInd;
                        data.JoinDupsInd = item.JoinDupsInd;
                        data.ConfgrtnEffStartTs = item.ConfgrtnEffStartTs;
                        data.ConfgrtnEffEndTs = item.ConfgrtnEffEndTs;
                        await _tableColConfigurationRepository.Update(_mapper.Map<Domain.Entities.TableColConfiguration>(data));
                    }
                    
                }
            }

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
