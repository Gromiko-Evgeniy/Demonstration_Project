using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService productService;

        public ProductController(IProductService productService) {
            this.productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            Console.WriteLine(HttpContext.Request.Cookies);
            return Ok( await productService.GetAllProducts());
        }
        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> GetAllExceptProductsForAdults()
        //{
        //    return Ok(await productService.GetAllProducts());
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProduct(int id)
        {
            var product = await productService.GetSingleProduct(id);
            if(product is null) {
                return NotFound("no such product found");
            }
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>AddProduct(AddUpdateProductDTO product)
        {
            Console.WriteLine(HttpContext.Request.Cookies);
            return Ok(await productService.AddProduct(product));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, AddUpdateProductDTO newProduct)
        {
            var product = await productService.UpdateProduct(id, newProduct);
            if (product is null)
            {
                return NotFound("no such product found");
            }
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await productService.DeleteProduct(id);
            if (result is null)
            {
                return NotFound("no such product found");
            }
            return Ok(result);
        }
    }
}
