using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRentingSystem.Models
{
    public class House
    {
        
        public int HouseId { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The Name must be numbers or letters and between 2 to 20 characters.")]
        [Display(Name = "Item name")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public double Area { get; set; }

        [RegularExpression(@"^[A-Za-z\s\-\.,']+", ErrorMessage = "City contains invalid characters or numbers.")]
        public string City { get; set; }

        [RegularExpression(@"^[A-Za-z0-9\s\-\.,']+", ErrorMessage = "Address contains invalid symbols.")]
        public string Address { get; set; }

        [Range(1, 20, ErrorMessage = "Number of rooms must be between 1 to 20.")]
        public int NumberOfRooms { get; set; }

        public bool Available { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual List<Order> OrderList { get; set; }

        public bool HasParking { get; set; }

        public bool IsFurnished { get; set; }

        public string LeaseDuration { get; set; }

        public bool UtilitiesIncluded { get; set; }

        public bool PetFriendly { get; set; }

        public string Amenities { get; set; }
    }
}
