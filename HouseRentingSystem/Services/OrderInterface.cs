using HouseRentingSystem.Models;


namespace HouseRentingSystem.DAL
{
    public interface OrderInterface
    {
        Task<bool> UpdateOrder(Order order);
        Task<IEnumerable<Order>?> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<bool> CreateOrder(Order order);
        Task<bool> DeleteOrder(int id);
    }
}
