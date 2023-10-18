using HouseRentingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using HouseRentingSystem.Services;
using System.Linq.Expressions;

namespace HouseRentingSystem.DAL
{
    public class HouseRepository : HouseInterface
    {
        private readonly ItemDBContext _db;
        private readonly ILogger<HouseRepository> _logger;

        public HouseRepository(ItemDBContext db, ILogger<HouseRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<House>> GetAllHouses()
        {
            try
            {
                return await _db.House.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[HouseRepository] house ToListAsync failed when GetAll was called, error message : {e}", ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<House>> GetAllAvailable()
        {
            try
            {
                return await _db.House.FromSqlRaw("SELECT* FROM house WHERE IsAvailable=1").ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[HouseRepository] house ToListAsync failed when GetAllAvailable was called, error message : {e}", ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<House>> GetAllWithFiltered(string city, int minArea, int maxArea, int minPrice, int maxPrice, int minRoomCount, int maxRoomCount)
        {
            try
            {
                var query = _db.House.AsQueryable();

                if (!string.IsNullOrEmpty(city))
                {
                    query = query.Where(h => EF.Functions.Like(h.City, $"%{city}%"));
                }
                if (minPrice > 0)
                {
                    query = query.Where(h => h.Price >= minPrice);
                }
                if (maxPrice > 0)
                {
                    query = query.Where(h => h.Price <= maxPrice);
                }
                if (minArea > 0)
                {
                    query = query.Where(h => h.Area >= minArea);
                }
                if (maxArea > 0)
                {
                    query = query.Where(h => h.Area <= maxArea);
                }
                if (minRoomCount > 0)
                {
                    query = query.Where(h => h.NumberOfRooms >= minRoomCount);
                }
                if (maxRoomCount > 0)
                {
                    query = query.Where(h => h.NumberOfRooms <= maxRoomCount);
                }

                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("[HouseRepo] house ToListAsync failed when GetAllFiltered was called, error message : {e}", e.Message);
                return null;
            }
        }

        public async Task<House> GetHouseById(int id)
        {
            try
            {
                return await _db.House.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[HouseRepository] house FindAsync failed when GetHouseById was called, error message : {e}", ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateHouse(House house)
        {
            try
            {
                _db.House.Add(house);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[HouseRepo] error with CreateHouse method, error message : {e}", ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateHouse(House house)
        {
            try
            {
                _db.House.Update(house);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[HouseRepository] error with UpdateHouse method, error message : {e}", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteHouse(int id)
        {
            try
            {
                var house = await _db.House.FindAsync(id);
                if (house == null)
                {
                    _logger.LogError("[HouseRepository] house does not exist for this id: " + id);
                    return false;
                }
                _db.House.Remove(house);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[HouseRepository] house deletion failed for the given id, error message: {e}", id, ex.Message);
                return false;
            }
        }

        public Task<House> GetAllFiltered(string city, int minArea, int maxArea, int minPrice, int maxPrice, int minRooms, int maxRooms)
        {
            throw new NotImplementedException();
        }
    }
}
