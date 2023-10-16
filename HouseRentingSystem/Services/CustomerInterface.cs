using HouseRentingSystem.Models;

namespace HouseRentingSystem.Services
{
    public interface ICustomerInterface
    {
        Task<bool> UpdateCustomer(Customer customer);
        Task<IEnumerable<Customer>?> GetAll();
        Task<Customer> GetCustomerById(int id);
        Task<bool> CreateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
    }
}