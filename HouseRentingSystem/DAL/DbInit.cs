using HouseRentingSystem.Models;
using Microsoft.AspNetCore.SignalR;
using System;

namespace HouseRentingSystem.DAL
{
    public class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            ItemDbContext context = serviceScope.ServiceProvider.GetRequiredService<ItemDbContext>();
            context.Database.EnsureCreated();

            if (!context.user.Any() && !context.customer.Any() && !context.owner.Any())
            {
                var user = new User
                {
                    Name = "Jonas Frankstein",
                    Address = "Holbergsplass3",
                    Email = "Jonas1@gmail.com",
                    BirthDate = new DateTime(1993, 6, 3),
                    PhoneNumber = 93655711
                };
                var customer = new Customer
                {
                    User = user
                };
                var owner = new Owner
                {
                    User = user,
                    AccountNumber = 52877738481,
                    AdCount = 0
                };

                context.AddRange(customer, owner);
                context.SaveChanges();
            }

            if (!context.house.Any())
            {
                var house = new List<House>
                {
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                    new House {Address="Osloveien18", Area=200, Description="bla bla bla", City="Oslo", IsAvailable=true, Price=400, RoomCount=4, IsFurnished=true, HasParking=false, ImageURL = "/Bilder/Pic1.jpg"},
                };

                var user = new User
                {
                    Name = "Leif Hansen ",
                    Address = "Nydalenveien33",
                    Email = "leif@gmail.com",
                    BirthDate = new DateTime(1995, 2, 1),
                    PhoneNumber = 456789546,
                };

                var owner = new Owner
                {
                    User = user,
                    AccountNumber = 4567899567,
                    AdCount = 0,
                    HouseList = houses
                };

                context.AddRange(owner);
                context.SaveChanges();
            }

            if (!context.order.Any())
            {
                var user = new User
                {
                    Name = "Andreas Tøen",
                    Address = "adgerveien22",
                    Email = "andreas1@gmail.com",
                    BirthDate = new DateTime(1983, 7, 2),
                    PhoneNumber = 45678788
                };

                var customer = new Customer
                {
                    User = user
                };

                context.Add(customer);
                context.SaveChanges();

                var order = new List<Order>
                {
                    new Order
                    {
                        Date = DateTime.Now,
                        PaymentMethod = "Card",
                        CustomerID = customer.CustomerID
                    },
                    new Order
                    {
                        Date = DateTime.Now,
                        PaymentMethod = "Klarna",
                        CustomerID = customer.CustomerID
                    }
                };

                var owner = new Owner
                {
                    User = user,
                    AccountNumber = 33333333333,
                    AdCount = 0
                };

                var house = new House
                {
                    Address = "Holbergsplass3",
                    Area = 200,
                    Description = "srdtfcygvuhbjkla",
                    City = "Oslo",
                    IsAvailable = true,
                    Price = 1600,
                    RoomCount = 4,
                    IsFurnished = true,
                    HasParking = false,
                    ImageURL = "~/Bilder/1.jpg",
                    Owner = owner
                };

                context.Add(house);
                context.SaveChanges();

                foreach (var o in order)
                {
                    o.HouseId = house.HouseId;
                }

                context.AddRange(order);
                context.SaveChanges();
            }
        }
    }
}
