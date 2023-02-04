using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;


namespace OnlineStore.DTOs.Order
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public string DeliveryAddress { get; set; } = "";
        public List<Models.Product> Products { get; set; } = new List<Models.Product>();
        public Models.User User { get; set; }
        public bool Paid { get; set; } = false;
    }
}
