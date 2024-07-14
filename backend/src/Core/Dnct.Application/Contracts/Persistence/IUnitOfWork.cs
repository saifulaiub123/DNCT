namespace Dnct.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IDatabaseSourcesRepository DatabaseSourcesRepository { get; }
    Task CommitAsync();
    ValueTask RollBackAsync();
}