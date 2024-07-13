using AutoMapper;
using Dnct.Application.Features.TreeView.Query.GetTableInstanceByDatabaseSourceId;
using Dnct.Domain.Model;


namespace Dnct.Application.Profiles
{
    public class TableConfigurationMapper : Profile
    {
        public TableConfigurationMapper()
        {
            CreateMap<TableConfigurationModel, GetTableInstanceByDatabaseSourceIdResponse>()
                .ForMember(src => src.Id, dest => dest.MapFrom(x => x.DatabaseSourceId))
                .ForMember(src => src.Title, dest => dest.MapFrom(x => x.InstanceName));
        }
    }
}