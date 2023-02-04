using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineStore.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; } = DateTime.Today;
        public int Age { 
            get { return DateOnly.FromDateTime(DateTime.Today).Year - DateOfBirth.Year; } 
        }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Money { get; set; } = 0;
        public Role Role { get; set; } = Role.Customer;
        //[JsonIgnore]
        public List<Order> Orders { get; set; } = new List<Order>();
    }

    public enum Role
    {
        Customer,
        Admin
    }
}
