
using Dnct.Domain.Model;

namespace Dnct.Application.Contracts.Persistence
{
    public interface IUserQueryRepository
    {
        Task<List<UserQueryModel>> GetUserQuries(int tableConfigId);
        Task<UserQueryModel> GetUserQueryByQueryId(int queryId);
        Task Create(UserQueryModel userQuery);
        Task Update(UserQueryModel userQuery);
        Task Delete(UserQueryModel userQuery);
    }
}
