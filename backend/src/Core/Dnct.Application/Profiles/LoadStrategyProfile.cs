using AutoMapper;
using Dnct.Domain.Model;
using Dnct.Application.Features.LoadStragegy.Commands.Create.AddNewLoadStrategy;
using Dnct.Application.Features.LoadStragegy.Query.GetAllLoadStrategy;

namespace Dnct.Application.Profiles
{
    public class LoadStrategyProfile : Profile
    {
        public LoadStrategyProfile()
        {
            CreateMap<LoadStrategyModel, GetAllLoadStrategyResponse>();
            CreateMap<AddNewLoadStrategyCommand, LoadStrategyModel>();
        }
    }
}