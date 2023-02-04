using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Services.OrderProduct
{
    public class OrderProductService : IOrderProductService
    {
        private IMapper mapper;
        private DataContext context;

        public OrderProductService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ResponseInfo<List<Models.OrderProduct>>> GetAllOrderProducts()
        {
            var orderProducts = await context.OrderProducts.Include(op => op.Order).Include(op => op.Product).ToListAsync();
            return new ResponseInfo<List<Models.OrderProduct>>(orderProducts, true, "all orders");
        }

        public async Task<ResponseInfo<GetOrderDTO>> AddProductToOrder(int productId, int orderId, int quantity = 1)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null)
            {
                return new ResponseInfo<GetOrderDTO>(null, false, "user not found");
            }

            var order = await context.Orders.Include(o => o.User).FirstOrDefaultAsync(p => p.Id == orderId);
            if (order is null)
            {
                return new ResponseInfo<GetOrderDTO>(null, false, "order not found");
            }

            var orderProduct = await context.OrderProducts.FirstOrDefaultAsync(op => op.ProductId == productId && op.OrderId == orderId);

            if (orderProduct is null)
            {
                var NewOrderProduct = new Models.OrderProduct() { Product = product, Order = order, ProductQuantity = quantity };
                context.OrderProducts.Add(NewOrderProduct);
            }
            else orderProduct.ProductQuantity += quantity;

            order.TotalPrice += product.Price * quantity;

            await context.SaveChangesAsync();
            return new ResponseInfo<GetOrderDTO>(mapper.Map<GetOrderDTO>(order), true, $"order with id {orderId}");
        }
        //remove
    }
}
