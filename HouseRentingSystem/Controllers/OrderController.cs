using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HouseRentingSystem.Services; // Assuming you have a service similar to OrdreInterface
using HouseRentingSystem.Models; // For the Order and House models
using HouseRentingSystem.ViewModels; // For the ItemListViewModel
using Microsoft.Extensions.Logging; // For the ILogger

namespace HouseRentingSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _orderLogger;
        private readonly OrderInterface _orderInterface; // This service replaces OrdreInterface
        private readonly HouseInterface _houseInterface; // This service is similar to HusInterface
        private readonly Receipt _receiptInterface; // Assuming you have a similar service for generating receipts

        public OrderController(OrderInterface orderInterface, ILogger<OrderController> logger, HouseInterface houseInterface, Receipt receiptInterface)
        {
            _orderInterface = orderInterface;
            _orderLogger = logger;
            _houseInterface = houseInterface;
            _receiptInterface = receiptInterface;
        }

        public async Task<IActionResult> Table()
        {
            var list = await _orderInterface.GetAllOrders(); // Assuming 'GetAllOrders' is a method in your service
            if (list == null)
            {
                _orderLogger.LogError("[OrderController] Order list not found during GetAllOrders call");
                return NotFound("Order list not found");
            }

            var itemListViewModel = new ItemListViewModel(list, "Table");
            return View(itemListViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderInterface.GetOrderById(id); // Assuming 'GetOrderById' is a method in your service
            if (order == null)
            {
                _orderLogger.LogError("[OrderController] Order not found for this ID: " + id);
                return NotFound("Order not found");
            }
            return View(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditConfirmed(Order order) // Order is assumed to be a model in your context
        {
            if (ModelState.IsValid)
            {
                bool success = await _orderInterface.UpdateOrder(order); // Assuming 'UpdateOrder' is a method in your service
                if (success)
                {
                    return RedirectToAction(nameof(Table));
                }
            }

            _orderLogger.LogWarning("[OrderController] Order update failed", order);
            return View(order);
        }

        // Assuming you have a method to create orders similar to 'lagOrdre' in "file 1"
        [HttpPost]
        [Authorize]
        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order, int houseId) // 'Order' and 'houseId' as parameters
        {
            if (ModelState.IsValid)
            {
                var house = await _houseInterface.GetHouseById(houseId); // Assuming 'GetHouseById' is a method in your service
                if (house == null)
                {
                    return NotFound("House does not exist!");
                }
                bool success = await _orderInterface.CreateOrder(order); // Assuming 'CreateOrder' is a method in your service
                if (success)
                {
                    // Assuming you have a method to generate PDF receipts similar to 'genererPdfKvittering' in "file 1"
                    var htmlReceipt = "<html><body><p><Receipt Details>.....</p></body></html>"; // Replace with actual receipt details
                    var pdfReceipt = _receiptInterface.GeneratePdfReceipt(htmlReceipt); // This method should return a byte array
                    var fileName = "Order Receipt.pdf";
                    return File(pdfReceipt, "application/pdf", fileName);
                }
            }

            _orderLogger.LogWarning("[OrderService] Failed to generate a receipt for this order", order);
            return RedirectToAction("Index"); // Assuming 'Index' is a method in your controller
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderInterface.GetOrderById(id); // Assuming 'GetOrderById' is a method in your service
            if (order == null)
            {
                _orderLogger.LogError("[OrderController] Order not found for this ID", id);
                return BadRequest("Order not found for given ID");
            }
            return View(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool success = await _orderInterface.DeleteOrder(id); // Assuming 'DeleteOrder' is a method in your service
            if (!success)
            {
                _orderLogger.LogError("[OrderController] Failed to delete order for this ID", id);
                return BadRequest("Failed to delete order");
            }
            return RedirectToAction(nameof(Table));
        }
    }
}
