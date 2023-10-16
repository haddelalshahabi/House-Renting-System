using HouseRentingSystem.Models;

namespace HouseRentingSystem.Services
{
    public interface OrderInterface
    {
        Task<bool> UpdateOrder(Order order);
        Task<IEnumerable<Order>?> GetAll();
        Task<Order> GetOrderWithId(int id);
        Task<bool> CreateOrder(Order order);
        Task<bool> DeleteOrder(int id);
    }
}
