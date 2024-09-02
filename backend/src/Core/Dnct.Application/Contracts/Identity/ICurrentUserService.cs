
namespace Dnct.Application.Contracts.Identity
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string Username { get; }
        string Email { get; }

    }
}
