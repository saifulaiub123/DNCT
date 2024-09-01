using AutoMapper;
using Dnct.Application.Features.DatabaseSource.Query.GetDatabaseSourcesById;
using Dnct.Application.Features.TreeView.Query.GetDatabasesByServerId;
using Dnct.Application.Features.TreeView.Query.GetTablesByDatabaseSourceId;
using Dnct.Domain.Entities;
using Dnct.Domain.Model;

namespace Dnct.Application.Profiles
{
    public class DatabaseSourcesMapper : Profile
    {
        public DatabaseSourcesMapper()
        {
            CreateMap<DatabaseSourceModel, GetDatabasesByServerIdResponse>()
                .ForMember(src => src.Id, dest => dest.MapFrom(x => x.DatabaseSourceId))
                .ForMember(src => src.Name, dest => dest.MapFrom(x => x.DataBaseName));

            CreateMap<DatabaseSourceModel, GetTablesByDatabaseSourceIdResponse>()
                .ForMember(src => src.Id, dest => dest.MapFrom(x => x.DatabaseSourceId))
                .ForMember(src => src.Name, dest => dest.MapFrom(x => x.TableName));

            CreateMap<DatabaseSources, GetDatabaseSourcesByIdResponse>()
                .ForMember(src => src.DatabaseSourceId, dest => dest.MapFrom(x => x.DatbsSrcId))
                .ForMember(src => src.ConnectionName, dest => dest.MapFrom(x => x.ConctnName))
                .ForMember(src => src.DatabaseName, dest => dest.MapFrom(x => x.TblDbsName));
        
        
        }
    }
}