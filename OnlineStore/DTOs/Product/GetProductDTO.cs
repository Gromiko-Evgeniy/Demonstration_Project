using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.DTOs.Product
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public Models.ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }

    }
}
