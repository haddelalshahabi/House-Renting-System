using HouseRentingSystem.Models;

namespace HouseRentingSystem.DAL
{
    public interface CustomerInterface
    {
        Task<bool> UpdateCustomer(Customer customer);
        Task<IEnumerable<Customer>?> GetAllCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<bool> CreateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
    }
}