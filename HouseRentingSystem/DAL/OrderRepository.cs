using Microsoft.EntityFrameworkCore;
using HouseRentingSystem.Models;
using HouseRentingSystem.Services;

namespace HouseRentingSystem.DAL
{
    public class OrderRepository : OrderInterface
    {

        private readonly ItemDBContext _db;
        private readonly ILogger<OrderInterface> _orderLogger;

        public OrderRepository(ItemDBContext db, ILogger<OrderInterface> logger)
        {
            _db = db;
            _orderLogger = logger;
        }

        public async Task<IEnumerable<Order>?> GetAllOrders()
        {
            try
            {
                return await _db.Order.ToListAsync();
            }
            catch (Exception ex)
            {
                _orderLogger.LogError("[OrderRepository] GetAll orders method failed on call, error message : {e}", ex.Message);
                return null;
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                return await _db.Order.FindAsync(id);
            }
            catch (Exception ex)
            {
                _orderLogger.LogError("[OrderRepository] Get order with id" + id + " method failed on call, error message : {e}", ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateOrder(Order order)
        {
            try
            {
                _db.Order.Add(order);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _orderLogger.LogError("[OrderRepository] Error with CreateOrder method, error message : {e}", ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            try
            {
                _db.Order.Update(order);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _orderLogger.LogError("[OrderRepository] Error with UpdateOrder method, error message : {e}", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                var order = await _db.Order.FindAsync(id);
                if (order == null)
                {
                    _orderLogger.LogError("[OrderRepository] Order does not exist for this id" + id);
                    return false;
                }

                _db.Order.Remove(order);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _orderLogger.LogError("[OrderRepository] Order deletion failed for the given id, error message {e}", id, ex.Message);
                return false;
            }
        }
    }
}
