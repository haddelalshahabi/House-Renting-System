using System;
using System.Collections.Generic;
using HouseRentingSystem.Models;

namespace HouseRentingSystem.ViewModels
{
    public class ItemListViewModel
    {
        public IEnumerable<House> House;
        public IEnumerable<Order> Order;
        public IEnumerable<Customer> Customer;
        public string? CurrentViewName;
        private House ListOfHouses;
        private string view;

        public ItemListViewModel(IEnumerable<House> house, string? viewName)
        {
            House = house;
            CurrentViewName = viewName;
        }

        public ItemListViewModel(IEnumerable<Customer> customer, string? viewName)
        {
            Customer = customer;
            CurrentViewName = viewName;
        }

        public ItemListViewModel(IEnumerable<Order> order, string? viewName)
        {
            Order = order;
            CurrentViewName = viewName;
        }

        public ItemListViewModel(House ListOfHouses, string view)
        {
            this.ListOfHouses = ListOfHouses;
            this.view = view;
        }

        public ItemListViewModel() { }
    }
}