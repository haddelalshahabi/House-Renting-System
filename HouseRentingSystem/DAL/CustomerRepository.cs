using Microsoft.EntityFrameworkCore;
using HouseRentingSystem.Models;

namespace HouseRentingSystem.DAL
{
    public class CustomerRepository : CustomerInterface
    {
        private readonly ItemDbContext _db;
        private readonly ILogger<CustomerRepository> _customerLogger;

        public CustomerRepository(ItemDbContext db, ILogger<CustomerRepository> logger)
        {
            _db = db;
            _customerLogger = logger;
        }

        public async Task<IEnumerable<Customer>?> GetAll()
        {
            try
            {
                return await _db.customer.ToListAsync();
            }
            catch (Exception ex)
            {
                _customerLogger.LogError("[CustomerRepository] GetAll customers method failed on call, error message : {e}", ex.Message);
                return null;
            }
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                return await _db.customer.FindAsync(id);
            }
            catch (Exception ex)
            {
                _customerLogger.LogError("[CustomerRepository] Get customer with id" + id + " method failed on call, error message : {e}", ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateCustomer(Customer customer)
        {
            try
            {
                _db.customer.Add(customer);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _customerLogger.LogError("[CustomerRepository] Error with CreateCustomer method, error message : {e}", ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            try
            {
                _db.customer.Update(customer);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _customerLogger.LogError("[CustomerRepository] Error with UpdateCustomer method, error message : {e}", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _db.customer.FindAsync(id);
                if (customer == null)
                {
                    _customerLogger.LogError("[CustomerRepository] Customer does not exist for this id" + id);
                    return false;
                }

                _db.customer.Remove(customer);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _customerLogger.LogError("[CustomerRepository] Customer deletion failed for the given id, error message {e}", id, ex.Message);
                return false;
            }
        }
    }
}
