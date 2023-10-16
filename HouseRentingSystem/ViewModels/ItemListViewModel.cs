using System;
using System.Collections.Generic;
using HouseRentingSystem.Models;

namespace HouseRentingSystem.ViewModels
{
    public class ItemListViewModel
    {
        public IEnumerable<House> Houses;
        public IEnumerable<Order> Orders;
        public IEnumerable<Customer> Customers;
        public string? CurrentViewName;
        private House houseList;
        private string viewName;

        public ItemListViewModel(IEnumerable<House> houses, string? currentViewName)
        {
            Houses = houses;
            CurrentViewName = currentViewName;
        }

        public ItemListViewModel(IEnumerable<Customer> customers, string? currentViewName)
        {
            Customers = customers;
            CurrentViewName = currentViewName;
        }

        public ItemListViewModel(IEnumerable<Order> orders, string? currentViewName)
        {
            Orders = orders;
            CurrentViewName = currentViewName;
        }

        public ItemListViewModel(House houseList, string viewName)
        {
            this.houseList = houseList;
            this.viewName = viewName;
        }

        public ItemListViewModel() { }
    }
}