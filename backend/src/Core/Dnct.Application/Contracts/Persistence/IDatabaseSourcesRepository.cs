

using Dnct.Domain.Entities;

namespace Dnct.Application.Contracts.Persistence
{
    public interface IDatabaseSourcesRepository
    {
        Task<List<DatabaseSources>> GetAllServer();
    }
}
