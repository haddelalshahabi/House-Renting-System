using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HouseRentingSystem.Models;

namespace HouseRentingSystem.Models
{
    public class Order
    {
        public Order() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ordreId { get; set; }

        public DateTime Dato { get; set; }

        [StringLength(200)]
        public string paymentMethod { get; set; }

        public virtual House house { get; set; }
        public int HouseId { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer customer { get; set; }

        // Original fields from "fil 2" preserved
        public virtual List<OrderItem>? OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
