using Microsoft.EntityFrameworkCore;
using HouseRentingSystem.Models;

namespace HouseRentingSystem.DAL
{
    public class OrderRepository : OrderInterface
    {

        private readonly ItemDbContext _db;
        private readonly ILogger<OrderInterface> _orderLogger;

        public OrderRepository(ItemDbContext db, ILogger<OrderInterface> logger)
        {
            _db = db;
            _orderLogger = logger;
        }

        public async Task<IEnumerable<Order>?> GetAll()
        {
            try
            {
                return await _db.order.ToListAsync();
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
                return await _db.order.FindAsync(id);
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
                _db.order.Add(order);
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
                _db.order.Update(order);
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
                var order = await _db.order.FindAsync(id);
                if (order == null)
                {
                    _orderLogger.LogError("[OrderRepository] Order does not exist for this id" + id);
                    return false;
                }

                _db.order.Remove(order);
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
