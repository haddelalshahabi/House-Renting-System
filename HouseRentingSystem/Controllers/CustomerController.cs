using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HouseRentingSystem.Models;
using HouseRentingSystem.ViewModels;
using HouseRentingSystem.DAL;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerInterface _customerInterface;

        public CustomerController(CustomerInterface customerInterface, ILogger<CustomerController> logger)
        {
            _customerInterface = customerInterface;
            _logger = logger;
        }

        public async Task<IActionResult> Table()
        {
            var customers = await _customerInterface.GetAllCustomers();
            if (customers == null)
            {
                _logger.LogError("[CustomerController] Customer list not found");
                return NotFound("Customer list not found");
            }

            var itemListViewModel = new ItemListViewModel(customers.ToList(), "Table");
            return View(itemListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerInterface.GetCustomerById(id);
            if (customer == null)
            {
                _logger.LogError($"[CustomerController] Customer not found for this ID: {id}");
                return NotFound("Customer not found");
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditConfirmed(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerInterface.UpdateCustomer(customer);
                return RedirectToAction(nameof(Table));
            }

            _logger.LogWarning("[CustomerController] Updating customer failed", customer);
            return View(customer);
        }

        [HttpGet]
        public IActionResult Create() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerInterface.CreateCustomer(customer);
                return RedirectToAction(nameof(Table));
            }

            _logger.LogWarning("[CustomerController] Customer creation failed", customer);
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerInterface.GetCustomerById(id);
            if (customer == null)
            {
                _logger.LogError($"[CustomerController] Customer not found for this ID: {id}");
                return BadRequest("Customer not found for given ID");
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _customerInterface.DeleteCustomer(id);
            if (result)
            {
                return RedirectToAction(nameof(Table));
            }

            _logger.LogError($"[CustomerController] Deleting customer failed for this ID: {id}");
            return BadRequest("Deleting customer failed");
        }
    }
}
