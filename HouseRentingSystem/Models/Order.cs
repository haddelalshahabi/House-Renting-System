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
        public int OrderId { get; set; }

        public DateTime Date { get; set; }

        [StringLength(200)]
        public string PaymentMethod { get; set; }

        public virtual House house { get; set; }
        public int HouseId { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer customerId { get; set; }

        public decimal PaidAmount { get; set; }
    }
}
