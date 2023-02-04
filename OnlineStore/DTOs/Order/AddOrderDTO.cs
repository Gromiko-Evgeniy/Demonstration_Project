using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.DTOs.Order
{
    public class AddOrderDTO
    {
        //public decimal TotalPrice { get; set; } = 0;
        public string DeliveryAddress { get; set; } = "";
        public int UserId { get; set; }
        //public bool Paid { get; set; } = false;
    }
}
