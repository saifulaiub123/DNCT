
using Dnct.Domain.Model;

namespace Dnct.Application.Contracts.Persistence
{
    public interface IUserQueryRepository
    {
        Task<List<UserQueryModel>> GetUserQuries();
        Task<UserQueryModel> GetUserQueryByQueryId(int queryId);
        Task Create(UserQueryModel userQuery);
        Task Update(UserQueryModel userQuery);
        Task Delete(UserQueryModel userQuery);
    }
}
