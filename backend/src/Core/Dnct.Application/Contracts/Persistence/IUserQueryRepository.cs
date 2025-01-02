
using Dnct.Domain.Model;

namespace Dnct.Application.Contracts.Persistence
{
    public interface IUserQueryRepository
    {
        Task<List<UserQueryModel>> GetUserQuries();
    }
}
