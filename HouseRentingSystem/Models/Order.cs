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
        public int OrdreId { get; set; }

        public DateTime Dato { get; set; }

        [StringLength(200)]
        public string paytmentMethod { get; set; }

        public virtual House house { get; set; }
        public int houseId { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer customer { get; set; }
    }
}
