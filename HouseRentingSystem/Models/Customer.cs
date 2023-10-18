using HouseRentingSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace HouseRentingSystem.Models;

public class Customer
{
    public Customer() { }

    [NotNull]
    public virtual User User { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }

    /*
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public DateTime Birthdate { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    public string Address { get; set; }
    */

    public virtual List<Order> Order { get; set; }
    
}