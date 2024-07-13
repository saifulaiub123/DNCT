using AutoMapper;
using Dnct.Application.Features.TreeView.Query.GetDatabasesByServerId;
using Dnct.Application.Features.TreeView.Query.GetTablesByDatabaseSourceId;
using Dnct.Domain.Model;

namespace Dnct.Application.Profiles
{
    public class DatabaseSourcesMapper : Profile
    {
        public DatabaseSourcesMapper()
        {
            CreateMap<DatabaseSourceModel, GetDatabasesByServerIdResponse>()
                .ForMember(src => src.Id, dest => dest.MapFrom(x => x.DatabaseSourceId))
                .ForMember(src => src.Title, dest => dest.MapFrom(x => x.DataBaseName));

            CreateMap<DatabaseSourceModel, GetTablesByDatabaseSourceIdResponse>()
                .ForMember(src => src.Id, dest => dest.MapFrom(x => x.DatabaseSourceId))
                .ForMember(src => src.Title, dest => dest.MapFrom(x => x.TableName));
        }
    }
}