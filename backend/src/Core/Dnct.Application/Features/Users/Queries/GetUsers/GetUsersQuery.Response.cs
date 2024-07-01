using Dnct.Application.Profiles;
using Dnct.Domain.Entities.User;

namespace Dnct.Application.Features.Users.Queries.GetUsers;

public record GetUsersQueryResponse : ICreateMapper<User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public int UserId { get; set; }
}