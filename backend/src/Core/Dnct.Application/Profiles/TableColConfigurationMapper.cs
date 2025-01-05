
using AutoMapper;
using Dnct.Application.Features.TreeView.Query.GetTableInstanceByDatabaseSourceId;
using Dnct.Application.Features.UserQuery.Query.GetAllTableColConfig;
using Dnct.Domain.Entities;
using Dnct.Domain.Model;

namespace Dnct.Application.Profiles
{
    public class TableColConfigurationMapper : Profile
    {
        public TableColConfigurationMapper()
        {
            CreateMap<TableColConfigurationModel, GetAllTableColConfigResponse>();
            CreateMap<TableColConfigurationModel, TableColConfiguration>();
        }
    }
}
