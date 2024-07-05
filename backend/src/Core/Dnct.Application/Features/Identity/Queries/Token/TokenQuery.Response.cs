
using Dnct.Application.Models.Jwt;

namespace Dnct.Application.Features.Identity.Queries.Token
{
    public class TokenQueryResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public AccessToken Token { get; set; }
    }
}
