

using Dnct.Application.Contracts.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Dnct.Infrastructure.Identity.UserManager
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(
        IHttpContextAccessor httpContextAccessor
    )
        {
            if (httpContextAccessor.HttpContext != null)
            {
                var claims = httpContextAccessor.HttpContext.User?.Claims;
                var enumerable = claims as Claim[] ?? claims.ToArray();

                var currentContext = httpContextAccessor.HttpContext;
                if (currentContext.User.Identity != null &&
                    (currentContext == null || !currentContext.User.Identity.IsAuthenticated)) return;
                //UserId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.First(c =>
                //    c.Type == ClaimTypes.Sid).Value);
                //Username = httpContextAccessor.HttpContext.User?.Claims?.First(c =>
                //    c.Type == ClaimTypes.NameIdentifier).Value;
                //Email = httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(c =>
                //    c.Type == ClaimTypes.Email)?.Value;
            }
        }


        public string Username { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
