using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await orderService.GetAllOrders());
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleOrder(int id)
        {
            return Ok(await orderService.GetSingleOrder(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDTO order)
        {
            return Ok(await orderService.AddOrder(order));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDTO newOrder)
        {
            var order = await orderService.UpdateOrder(id, newOrder);
            return Ok(order);
        }

        [Authorize]
        [HttpPut("{orderId}/{userId}")]
        public async Task<IActionResult> PayOrder(int orderId, int userId)
        {
            return Ok(await orderService.PayOrder(orderId, userId)); 
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(await orderService.DeleteOrder(id));
        }
    }
}
