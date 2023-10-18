using Microsoft.AspNetCore.Mvc;
using HouseRentingSystem.Services; // This assumes you have a similar service layer as in "file 1"
using HouseRentingSystem.ViewModels; // This assumes you have defined a similar ViewModel as in "file 1"
using System.Threading.Tasks;
using HouseRentingSystem.DAL;

namespace HouseRentingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly HouseInterface _houseInterface; // Renamed from 'HusInterface' to 'IHouseService' for clarity and adherence to naming conventions

        public HomeController(HouseInterface houseInterface)
        {
            // this._houseInterface = houseService;
            _houseInterface = houseInterface;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var list = await _houseInterface.GetAllHouses(); // Assuming 'GetAllHouses' is a method in your service, similar to 'hentAlle' in "file 1"
            if (list == null)
            {
                return NotFound("House list not found");
            }

            var itemListViewModel = new ItemListViewModel(list, "Table"); // Assuming you have a similar ViewModel as in "file 1"
            return View(itemListViewModel);
        }
    }
}