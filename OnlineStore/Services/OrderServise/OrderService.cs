using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private IMapper mapper;
        private DataContext context;

        public OrderService(IMapper mapper, DataContext context) { 
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ResponseInfo<List<GetOrderDTO>>> GetAllOrders()
        {
            var orders = await context.Orders.Include(o => o.User).ToListAsync();
            var orderDTOs = orders.Select(p => mapper.Map<GetOrderDTO>(p)).ToList();
            if (orderDTOs.Count == 0) {
                return new ResponseInfo<List<GetOrderDTO>>(orderDTOs, true, "no orders yet");
            }
            return new ResponseInfo<List<GetOrderDTO>>(orderDTOs, true, "all orders");
        }

        public async Task<ResponseInfo<GetOrderDTO>> GetSingleOrder(int id)
        {
            var order = await context.Orders.Include(o => o.User).FirstOrDefaultAsync(p => p.Id == id);
            if (order is null) {
                return new ResponseInfo<GetOrderDTO>(null, false, "order not found");
            }
            return new ResponseInfo<GetOrderDTO>(mapper.Map<GetOrderDTO>(order), true, $"order with id {id}");
        }

        public async Task<ResponseInfo<List<GetOrderDTO>>> AddOrder(AddOrderDTO order)
        {
            var newOrder = mapper.Map<Order>(order);
            var user = context.Users.FirstOrDefault(u => u.Id == order.UserId);
            if(user is null) {
                return new ResponseInfo<List<GetOrderDTO>>(null, false, "user not found");
            }
            newOrder.User = user;

            context.Orders.Add(newOrder);
            await context.SaveChangesAsync();

            var orders = await context.Orders.Include(o => o.User).ToListAsync();

            var orderDTOs = orders.Select(p => mapper.Map<GetOrderDTO>(p)).ToList();
            return new ResponseInfo<List<GetOrderDTO>>(orderDTOs, true, "all orders");
        }

        public async Task<ResponseInfo<GetOrderDTO>> UpdateOrder(int id, UpdateOrderDTO newOrder)
        {
            var order = await context.Orders.FirstOrDefaultAsync(p => p.Id == id);
            if (order is null)
            {
                return new ResponseInfo<GetOrderDTO>(null, false, "order not found");
            }

            //order.TotalPrice = newOrder.TotalPrice;
            order.DeliveryAddress = newOrder.DeliveryAddress;

            await context.SaveChangesAsync();

            return new ResponseInfo<GetOrderDTO>(mapper.Map<GetOrderDTO>(order), true, $"order with id {id}");
        }


        public async Task<ResponseInfo<List<GetOrderDTO>>> PayOrder(int orderId, int userId)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order is null) {
                return new ResponseInfo<List<GetOrderDTO>>(null, false, "order not found");
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null) {
                return new ResponseInfo<List<GetOrderDTO>>(null, false, "user not found");
            }

            if (user.Money < order.TotalPrice) {
                new ResponseInfo<GetOrderDTO>(null, false, "not enough money");
            }
            user.Money -= order.TotalPrice;
            order.Paid = true;
            await context.SaveChangesAsync();
            return await GetAllOrders();
        }

        public async Task<ResponseInfo<List<GetOrderDTO>>> DeleteOrder(int id)
        {
            var order = await context.Orders.FirstOrDefaultAsync(p => p.Id == id);
            if (order is null)
            {
                return new ResponseInfo<List<GetOrderDTO>>(null, false, "order not found");
            }
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
            var orders = await context.Orders.Include(o => o.User).ToListAsync();
            var orderDTOs = orders.Select(p => mapper.Map<GetOrderDTO>(p)).ToList();
            return new ResponseInfo<List<GetOrderDTO>>(orderDTOs, true, "all orders");
        }
    }
}
