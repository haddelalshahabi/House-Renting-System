using HouseRentingSystem.Models;

namespace HouseRentingSystem.Services
{
    public interface IOwnerInterface
    {
        Task<IEnumerable<Owner>?> GetAllOwners();
    }
}
