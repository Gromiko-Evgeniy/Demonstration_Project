namespace OnlineStore.Services.OrderProduct
{
    public interface IOrderProductService
    {
        public Task<ResponseInfo<GetOrderDTO>> AddProductToOrder(int productId, int orderId, int quantity = 1);
        public Task<ResponseInfo<List<Models.OrderProduct>>> GetAllOrderProducts();
    }
}
