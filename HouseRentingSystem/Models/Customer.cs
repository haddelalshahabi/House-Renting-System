using HouseRentingSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System;
namespace HouseRentingSystem.Models;

public class Customer
{
        public Customer() { }

        [NotNull]
        public virtual Users Individual { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
