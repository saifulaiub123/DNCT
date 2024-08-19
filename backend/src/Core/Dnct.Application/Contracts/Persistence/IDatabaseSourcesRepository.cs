

using Dnct.Domain.Entities;
using Dnct.Domain.Model;

namespace Dnct.Application.Contracts.Persistence
{
    public interface IDatabaseSourcesRepository
    {
        Task<List<DatabaseSourceModel>> GetDatabasesByServerId(int id);
        Task<List<DatabaseSourceModel>> GetDatabasesByServerIds(List<int> ids);
        Task<List<DatabaseSourceModel>> GetTablesByDatabaseSourceId(int databaseSourceId);

        Task<List<DatabaseSourceModel>> GetTableInstance(string connection, string databaseName, string tableName);
        Task CrateTable(DatabaseSources table);
    }
}
