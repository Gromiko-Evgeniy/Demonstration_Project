using OnlineStore.DTOs.Order;


namespace OnlineStore.Services.OrderService
{
    public interface IOrderService
    {
        Task<ResponseInfo<List<GetOrderDTO>>> GetAllOrders();
        Task<ResponseInfo<GetOrderDTO>> GetSingleOrder(int id);
        Task<ResponseInfo<List<GetOrderDTO>>> AddOrder(AddOrderDTO product);
        Task<ResponseInfo<GetOrderDTO>> UpdateOrder(int id, UpdateOrderDTO product);
        Task<ResponseInfo<List<GetOrderDTO>>> DeleteOrder(int id);
        Task<ResponseInfo<List<GetOrderDTO>>> PayOrder(int orderId, int userId);
    }
}
