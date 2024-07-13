
namespace Dnct.Application.Contracts.Persistence
{
    public interface IConnectionMasterRepository
    {
        Task<List<ConnectionsMaster>> GetAllServer();
    }
}
