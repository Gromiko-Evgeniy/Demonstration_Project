using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; } = 0;
        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        //[JsonIgnore]
        //public List<Order> Orders { get; set; }
        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
