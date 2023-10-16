using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HouseRentingSystem.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HouseRentingSystem.Controllers
{
    [Authorize] // Ensure the entire controller requires authorization
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger; // Assuming you want to use the built-in ILogger
        private readonly CustomerInterface _customerInterface;

        public CustomerController(CustomerInterface _customerInterface, ILogger<CustomerController> logger)
        {
            _customerInterface = Interface;
            _logger = logger;
        }

        public async Task<IActionResult> Table()
        {
            var customers = await _customerInterface.Customers.ToListAsync();
            if (customers == null)
            {
                _logger.LogError("[CustomerController] Customer list not found");
                return NotFound("Customer list not found");
            }

            // Assuming there is a ViewModel called "ItemListViewModel" similar to "file 1"
            var itemListViewModel = new ItemListViewModel(customers, "Table");
            return View(itemListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) // Changed from 'Endre' to 'Edit'
        {
            var customer = await _customerInterface.Customers.FindAsync(id);
            if (customer == null)
            {
                _logger.LogError($"[CustomerController] Customer not found for this ID: {id}");
                return NotFound("Customer not found");
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditConfirmed(Customer customer) // Changed from 'EndreBekreftet' to 'EditConfirmed'
        {
            if (ModelState.IsValid)
            {
                _customerInterface.Update(customer); // Updating the customer
                await _customerInterface.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
            }

            _logger.LogWarning("[CustomerController] Updating customer failed", customer);
            return View(customer);
        }

        [HttpGet]
        public IActionResult Create() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer) // Changed from 'Lag' to 'Create'
        {
            if (ModelState.IsValid)
            {
                await _customerInterface.Customers.AddAsync(customer);
                await _customerInterface.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
            }

            _logger.LogWarning("[CustomerController] Customer creation failed", customer);
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) // Changed from 'Slett' to 'Delete'
        {
            var customer = await _customerInterface.Customers.FindAsync(id);
            if (customer == null)
            {
                _logger.LogError($"[CustomerController] Customer not found for this ID: {id}");
                return BadRequest("Customer not found for given ID");
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id) // Changed from 'SlettBekreftet' to 'DeleteConfirmed'
        {
            var customer = await _customerInterface.Customers.FindAsync(id);
            if (customer != null)
            {
                _customerInterface.Customers.Remove(customer);
                await _customerInterface.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
            }

            _logger.LogError($"[CustomerController] Deleting customer failed for this ID: {id}");
            return BadRequest("Deleting customer failed");
        }
    }
}
