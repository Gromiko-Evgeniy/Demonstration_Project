using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private IProductTypeService productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await productTypeService.GetAllProductTypes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProduct(int id)
        {
            var product = await productTypeService.GetSingleProductType(id);
            if (product is null)
            {
                return NotFound("no such product found");
            }
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(AddUpdateProductTypeDTO product)
        {
            return Ok(await productTypeService.AddProductType(product));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, AddUpdateProductTypeDTO newProduct)
        {
            var product = await productTypeService.UpdateProductType(id, newProduct);
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
            var result = await productTypeService.DeleteProductType(id);
            if (result is null)
            {
                return NotFound("no such product found");
            }
            return Ok(result);
        }
    }
}
