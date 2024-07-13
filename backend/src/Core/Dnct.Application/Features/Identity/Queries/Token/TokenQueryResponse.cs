
using Dnct.Application.Models.Jwt;

namespace Dnct.Application.Features.Identity.Queries.Token
{
    public class TokenQueryResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public AuthToken Token { get; set; }
    }
}
