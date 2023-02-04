using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.OrderProduct;
using OnlineStore.Services.ProductService;

namespace OnlineStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private IOrderProductService orderProductService;

        public OrderProductController(IOrderProductService orderProductService)
        {
            this.orderProductService = orderProductService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            Console.WriteLine(HttpContext.Request.Cookies);
            return Ok(await orderProductService.GetAllOrderProducts());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProductToOrder(AddOrderProductDTO data)
        {
            return Ok(await orderProductService.AddProductToOrder(data.ProductId, data.OrderId, data.Quantity));
        }
    }
}
