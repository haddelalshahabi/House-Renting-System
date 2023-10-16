using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HouseRentingSystem.Models
{
    public class Users
    {
        public Users() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The name must contain between 2 and 20 characters.")]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        [RegularExpression(@"^[A-Za-z0-9\s\-\.,']+", ErrorMessage = "Address contains invalid symbols.")]
        public string Address { get; set; }

        [RegularExpression(@"^\d{8}$")]
        public long PhoneNumber { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string Email { get; set; }
    }
}
