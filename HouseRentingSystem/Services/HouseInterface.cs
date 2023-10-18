using HouseRentingSystem.Models;

namespace HouseRentingSystem.DAL
{
    public interface HouseInterface
    {
        Task<IEnumerable<House>> GetAllHouses();
        Task<House> GetHouseById(int id);

        Task<House> GetAllFiltered(string city, int minArea, int maxArea, int minPrice, int maxPrice, int minRooms, int maxRooms);
        Task<bool> CreateHouse(House house);
        Task<bool> UpdateHouse(House house);
        Task<bool> DeleteHouse(int id);
    }
}