using AutoMapper;
using Dnct.Application.Features.Server.Query.GetServerInfo;

namespace Dnct.Application.Profiles
{
    public class ConnectionMasterMapper : Profile
    {
        public ConnectionMasterMapper()
        {
            CreateMap<ConnectionsMaster, GetAllServerResponse>()
                .ForMember(src => src.Id, dest => dest.MapFrom(x => x.ContnId))
                .ForMember(src => src.Title, dest => dest.MapFrom(x => $"{x.ContnName}({x.HostIp})"));
        }
    }
}
