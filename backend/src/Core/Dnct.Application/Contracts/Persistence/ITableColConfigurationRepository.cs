using Dnct.Domain.Entities;
using Dnct.Domain.Entities.Order;
using Dnct.Domain.Model;

namespace Dnct.Application.Contracts.Persistence;

public interface ITableColConfigurationRepository
{
    Task<List<TableColConfigurationModel>> GetAll(int tableConfigId);
    Task<TableColConfigurationModel> GetById(int tblColConfigId, int tblConfigId);
    Task Create(TableColConfiguration tblColConfig);
    Task Update(TableColConfiguration tblColConfig);
    Task Delete(int tblColConfigId, int tblConfigId);
}