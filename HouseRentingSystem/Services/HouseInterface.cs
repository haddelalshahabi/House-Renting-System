using HouseRentingSystem.Models;

namespace HouseRentingSystem.Services
{
    public interface IHouseInterface
    {
        Task<IEnumerable<House>> GetAll();
        Task<House> GetHouseById(int id);

        Task<House> GetAllWithFilter(string city, int minArea, int maxArea, int minPrice, int maxPrice, int minRooms, int maxRooms);
        Task<bool> Create(House house);
        Task<bool> Update(House house);
        Task<bool> Delete(int id);
    }
}
