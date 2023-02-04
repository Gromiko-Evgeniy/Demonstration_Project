using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; } = 0; 
        public string DeliveryAddress { get; set; } = "";
        //public List<Product> Products { get; set; } = new List<Product>();
        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }
        public bool Paid { get; set; } = false;
    }
}
