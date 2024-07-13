

using Dnct.Domain.Entities;
using Dnct.Domain.Model;

namespace Dnct.Application.Contracts.Persistence
{
    public interface IDatabaseSourcesRepository
    {
        Task<List<DatabaseSourceModel>> GetDatabasesByServerId(int id);
        Task<List<DatabaseSourceModel>> GetTablesByDatabaseSourceId(int databaseSourceId);
    }
}
