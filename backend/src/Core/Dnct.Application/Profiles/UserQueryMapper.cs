using AutoMapper;
using Dnct.Application.Features.UserQuery.Query.GetUserQuery;
using Dnct.Domain.Model;

namespace Dnct.Application.Profiles
{
    public class UserQueryMapper : Profile
    {
        public UserQueryMapper()
        {
            CreateMap<UserQueryModel, GetUserQueryResponse>();
        }
    }
}