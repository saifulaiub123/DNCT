using Dnct.Domain.Entities.Order;

namespace Dnct.Application.Contracts.Persistence;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);
    Task<List<Order>> GetAllUserOrdersAsync(int userId);
    Task<List<Order>> GetAllOrdersWithRelatedUserAsync();
    Task<Order> GetUserOrderByIdAndUserIdAsync(int userId,int orderId,bool trackEntity);
    Task DeleteUserOrdersAsync(int userId);
}