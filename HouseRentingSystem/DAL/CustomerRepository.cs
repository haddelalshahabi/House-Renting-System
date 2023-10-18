using Microsoft.EntityFrameworkCore;
using HouseRentingSystem.Models;
using HouseRentingSystem.Services;

namespace HouseRentingSystem.DAL
{
    public class CustomerRepository : CustomerInterface
    {
        private readonly ItemDBContext _db;
        private readonly ILogger<CustomerRepository> _customerLogger;

        public CustomerRepository(ItemDBContext db, ILogger<CustomerRepository> logger)
        {
            _db = db;
            _customerLogger = logger;
        }

        public async Task<IEnumerable<Customer>?> GetAllCustomers()
        {
            try
            {
                return await _db.Customer.ToListAsync();
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
                return await _db.Customer.FindAsync(id);
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
                _db.Customer.Add(customer);
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
                _db.Customer.Update(customer);
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
                var customer = await _db.Customer.FindAsync(id);
                if (customer == null)
                {
                    _customerLogger.LogError("[CustomerRepository] Customer does not exist for this id" + id);
                    return false;
                }

                _db.Customer.Remove(customer);
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
