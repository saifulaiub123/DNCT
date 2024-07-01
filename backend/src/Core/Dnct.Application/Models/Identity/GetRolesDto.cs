using Dnct.Application.Profiles;
using Dnct.Domain.Entities.User;

namespace Dnct.Application.Models.Identity;

public class GetRolesDto:ICreateMapper<Role>
{
    public string Id { get; set; }
    public string Name { get; set; }
}