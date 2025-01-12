using Dnct.Domain.Model;

namespace Dnct.Application.Contracts.Persistence
{
    public interface IRunTimeParametersMasterRepository
    {
        Task<List<RunTimeParametersMasterModel>> GetAll(int tableConfigId);
        Task Create(List<RunTimeParametersMasterModel> models);
    }
}
