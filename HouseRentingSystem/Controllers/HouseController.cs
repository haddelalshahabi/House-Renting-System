using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HouseRentingSystem.Services; // Assuming you have a service similar to HusInterface
using HouseRentingSystem.Models; // For the House model
using HouseRentingSystem.ViewModels; // For the ItemListViewModel
using HouseRentingSystem.DAL;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : Controller
    {
        private readonly ILogger<HouseController> _houseLogger;
        private readonly HouseInterface _houseInterface; // This service replaces HusInterface

        public HouseController(HouseInterface houseInterface, ILogger<HouseController> logger)
        {
            _houseInterface = houseInterface;
            _houseLogger = logger;
        }

        public async Task<IActionResult> Table()
        {
            var list = await _houseInterface.GetAllHouses();
            if (list == null)
            {
                _houseLogger.LogError("[HouseController] House list not found when GetAll() was called");
                return NotFound("House list not found");
            }

            var itemListViewModel = new ItemListViewModel(list, "Table");
            return View(itemListViewModel);
        }

        public async Task<IActionResult> AvailableHouses()
        {
            var list = await _houseInterface.GetAllHouses();
            if (list == null)
            {
                _houseLogger.LogError("[HouseController] House list not found when GetAll() was called");
                return NotFound("House list not found");
            }

            var itemListViewModel = new ItemListViewModel(list, "Grid");
            return View(itemListViewModel);
        }

        public async Task<IActionResult> FetchWithFilter(string city, int minArea, int maxArea, int minPrice, int maxPrice, int minRooms, int maxRooms)
        {
            var list = await _houseInterface.GetAllFiltered(city, minArea, maxArea, minPrice, maxPrice, minRooms, maxRooms);
            if (list == null)
            {
                return NotFound("Nothing found");
            }

            var itemListViewModel = new ItemListViewModel(list, "Table");
            return View(itemListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var house = await _houseInterface.GetHouseById(id);
            if (house == null)
            {
                _houseLogger.LogError("[HouseController] House not found with this ID: " + id);
                return NotFound("House not found");
            }

            return View(house);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(House house)
        {
            if (ModelState.IsValid)
            {
                bool success = await _houseInterface.CreateHouse(house);
                if (success)
                {
                    return RedirectToAction(nameof(Table));
                }
            }
            _houseLogger.LogWarning("[HouseController] Failed to create house", house);
            return View(house);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditHouse(int id)
        {
            var house = await _houseInterface.GetHouseById(id);
            if (house == null)
            {
                _houseLogger.LogError("[HouseController] House not found with this ID: " + id);
                return NotFound("House not found");
            }
            return View(house);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditConfirmed(House house)
        {
            if (ModelState.IsValid)
            {
                bool success = await _houseInterface.UpdateHouse(house);
                if (success)
                {
                    return RedirectToAction(nameof(Table));
                }
                else
                {
                    _houseLogger.LogWarning("[HouseController] Failed to modify the house. House ID: " + house.HouseId); // Assuming house has a property Id
                    ModelState.AddModelError(string.Empty, "Failed to modify the house. Please try again.");
                }
            }
            else
            {
                _houseLogger.LogWarning("[HouseController] Invalid model state.");
                // Log more details about the model state errors if needed
                foreach (var modelStateKey in ViewData.ModelState.Keys)
                {
                    var modelStateVal = ViewData.ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        // Log your modelState errors
                        _houseLogger.LogWarning($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }
            }
            return View(house); // If update fails, stay on the current view with validation feedback
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteHouse(int id)
        {
            var house = await _houseInterface.GetHouseById(id);
            if (house == null)
            {
                _houseLogger.LogError("[HouseController] House not found with this ID: " + id);
                return NotFound("House not found");
            }
            return View(house);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool success = await _houseInterface.DeleteHouse(id);
            if (!success)
            {
                _houseLogger.LogWarning("[HouseController] Failed to delete house with ID: " + id);
                return BadRequest("Failed to delete house");
            }
            return RedirectToAction(nameof(Table));
        }
    }
}
