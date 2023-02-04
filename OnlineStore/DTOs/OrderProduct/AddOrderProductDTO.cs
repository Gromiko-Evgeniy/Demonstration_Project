namespace OnlineStore.DTOs.OrderProduct
{
    public class AddOrderProductDTO
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
