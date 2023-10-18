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

        public virtual List<House> ListOfHouses { get; set; }

        [NotNull]
        public virtual User User { get; set; }

        public int NumberOfAdvertisements { get; set; }
    }
}
