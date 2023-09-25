using HouseRentingSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseRentingSystem.ViewModels;

public class CreateOrderItemViewModel
{
    public OrderItem OrderItem { get; set; } = default!;
    public List<SelectListItem> ItemSelectList { get; set; } = default!;
    public List<SelectListItem> OrderSelectList { get; set; } = default!;
}