using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.DTOs.Product
{
    public class AddUpdateProductDTO
    {
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public int ProductTypeId { get; set; }
    }
}
