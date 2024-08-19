

using Dnct.Domain.Model;

namespace Dnct.Application.Contracts.Persistence
{
    public interface ITableConfigurationRepository
    {
        Task<List<TableConfigurationModel>> GetTableInstanceByDatabaseSourceId(int databaseSourceId);
    }
}
