using Dnct.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Dnct.Domain.Entities.User;

public class UserToken:IdentityUserToken<int>,IEntity
{
    public UserToken()
    {
        GeneratedTime=DateTime.Now;
    }

    public User User { get; set; }
    public DateTime GeneratedTime { get; set; }

}