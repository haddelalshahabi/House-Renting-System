using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace HouseRentingSystem.Models
{
    public class Owner
    {
        public Owner() { }

        [RegularExpression(@"^\d{11}$")]
        [Key]
        public long AccountNumber { get; set; }

        public virtual List<House> HouseList { get; set; }

        [NotNull]
        public virtual Users Users { get; set; }

        public int AdCount { get; set; }
    }
}
