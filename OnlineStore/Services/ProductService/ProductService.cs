using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System.Linq;

namespace OnlineStore.Services.ProductService
{
    public class ProductService : IProductService
    {
        private IMapper mapper;
        private DataContext context;

        public ProductService(IMapper mapper, DataContext context) { 
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ResponseInfo<List<GetProductDTO>>> GetAllProducts()
        {
            var products = await context.Products.Include(o => o.ProductType).ToListAsync();
            var productDTOs = products.Select(p => mapper.Map<GetProductDTO>(p)).ToList();
            if (productDTOs.Count == 0)
            {
                return new ResponseInfo<List<GetProductDTO>>(productDTOs, false, "no products yet");
            }
            return new ResponseInfo<List<GetProductDTO>>(productDTOs, true, "all products");
        }

        public async Task<ResponseInfo<GetProductDTO>> GetSingleProduct(int id) //отдельный запрос для алкоголя?
        {
            var product = await context.Products.Include(o => o.ProductType).FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return new ResponseInfo<GetProductDTO>(null, false, "product not found");
            }
            return new ResponseInfo<GetProductDTO>(mapper.Map<GetProductDTO>(product), true, $"product with id {id}");
        }

        public async Task<ResponseInfo<List<GetProductDTO>>> AddProduct(AddUpdateProductDTO product)
        {
            var newProduct = mapper.Map<Product>(product);
            var type = context.ProductTypes.FirstOrDefault(t => t.Id == product.ProductTypeId);
            if(type is null) {
                return new ResponseInfo<List<GetProductDTO>>(null, false, "product type not found");
            }
            newProduct.ProductType = type;

            context.Products.Add(newProduct);
            await context.SaveChangesAsync();

            var products = await context.Products.Include(p => p.ProductType).ToListAsync();

            var productDTOs = products.Select(p => mapper.Map<GetProductDTO>(p)).ToList();
            return new ResponseInfo<List<GetProductDTO>>(productDTOs, true, "all products");
        }

        public async Task<ResponseInfo<GetProductDTO>> UpdateProduct(int id, AddUpdateProductDTO productDTO)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return new ResponseInfo<GetProductDTO>(null, false, "product not found");
            }

            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.Discount = productDTO.Discount;
            product.Quantity = productDTO.Quantity;
            product.Description = productDTO.Description;

            if (product.ProductTypeId != productDTO.ProductTypeId)
            {
                var type = context.ProductTypes.FirstOrDefault(t => t.Id == productDTO.ProductTypeId);
                if (type is null)
                {
                    return new ResponseInfo<GetProductDTO>(null, false, "product type not found");
                }
                product.ProductType = type;
            }

            await context.SaveChangesAsync();

            return new ResponseInfo<GetProductDTO>(mapper.Map<GetProductDTO>(product), true, $"product with id {id}");
        }

        public async Task<ResponseInfo<List<GetProductDTO>>> DeleteProduct(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) {
                return new ResponseInfo<List<GetProductDTO>>(null, false, "product not found");
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();

            var products = await context.Products.Include(p => p.ProductType).ToListAsync();

            var productDTOs = products.Select(p => mapper.Map<GetProductDTO>(p)).ToList();
            return new ResponseInfo<List<GetProductDTO>>(productDTOs, true, "all products");
        }

        //public async Task<ResponseInfo<GetOrderDTO>> RemoveProductFromOrder(int productId, int orderId)
        //{

        //    var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        //    if (product is null)
        //    {
        //        return new ResponseInfo<GetOrderDTO>(null, false, "user not found");
        //    }

        //    var order = await context.Orders.Include(o => o.User).FirstOrDefaultAsync(p => p.Id == orderId);
        //    if (order is null)
        //    {
        //        return new ResponseInfo<GetOrderDTO>(null, false, "order not found");
        //    }

        //    order.Products.Remove(product);

        //    //context.OrderProducts.Remove(orderProduct); //нужно ли ?

        //    await context.SaveChangesAsync();
        //    return new ResponseInfo<GetOrderDTO>(mapper.Map<GetOrderDTO>(order), true, $"order with id {orderId}");
        //}
    }
}
