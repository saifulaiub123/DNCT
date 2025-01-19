using Dnct.Domain.Entities;
using Dnct.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnct.Application.Contracts.Persistence
{
    public interface ILoadStrategyRepository
    {
        Task<List<LoadStrategyModel>> GetAll(int tableConfigId);
        Task<LoadStrategyModel> Get(int tableConfigId, int loadStrategyId);
        Task Create(TblLoadStrategyModel model);
    }
}
