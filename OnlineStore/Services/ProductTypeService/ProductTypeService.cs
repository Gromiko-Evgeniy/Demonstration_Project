using Microsoft.EntityFrameworkCore;
using OnlineStore.DTOs.ProductType;

namespace OnlineStore.Services.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
        private IMapper mapper;
        private DataContext context;

        public ProductTypeService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ResponseInfo<List<GetProductTypeDTO>>> GetAllProductTypes()
        {
            var types = await context.ProductTypes.ToListAsync();
            var typeDTOs = types.Select(p => mapper.Map<GetProductTypeDTO>(p)).ToList();
            if (typeDTOs.Count == 0)
            {
                return new ResponseInfo<List<GetProductTypeDTO>>(typeDTOs, true, "no types yet");
            }  
            return new ResponseInfo<List<GetProductTypeDTO>>(typeDTOs, true, "all types");
        }

        public async Task<ResponseInfo<GetProductTypeDTO>> GetSingleProductType(int id)
        {
            var type = await context.ProductTypes.FirstOrDefaultAsync(p => p.Id == id);
            if (type is null) {
                return new ResponseInfo<GetProductTypeDTO>(null, false, "type not found");
            }
            return new ResponseInfo<GetProductTypeDTO>(mapper.Map<GetProductTypeDTO>(type), true, $"product with id {id}");
        }

        public async Task<ResponseInfo<List<GetProductTypeDTO>>> AddProductType(AddUpdateProductTypeDTO type)
        {
            var newProductType = mapper.Map<Product>(type);
            context.Products.Add(newProductType);
            await context.SaveChangesAsync();

            var types = await context.ProductTypes.ToListAsync();
            var typeDTOs = types.Select(p => mapper.Map<GetProductTypeDTO>(p)).ToList();
            if (typeDTOs.Count == 0)
            {
                return new ResponseInfo<List<GetProductTypeDTO>>(typeDTOs, true, "no types yet");
            }
            return new ResponseInfo<List<GetProductTypeDTO>>(typeDTOs, true, "all types");
        }

        public async Task<ResponseInfo<GetProductTypeDTO>> UpdateProductType(int id, AddUpdateProductTypeDTO typeDTO)
        {
            var type = await context.ProductTypes.FirstOrDefaultAsync(p => p.Id == id);
            if (type is null)
            {
                return new ResponseInfo<GetProductTypeDTO>(null, false, "type not found");
            }
            type.TypeName = typeDTO.TypeName;
            type.AgeLimit = typeDTO.AgeLimit;

            await context.SaveChangesAsync();

            return new ResponseInfo<GetProductTypeDTO>(mapper.Map<GetProductTypeDTO>(type), true, $"type with id {id}");
        }
        public async Task<ResponseInfo<List<GetProductTypeDTO>>> DeleteProductType(int id)
        {
            var type = await context.ProductTypes.FirstOrDefaultAsync(p => p.Id == id);
            if (type is null)
            {
                return new ResponseInfo<List<GetProductTypeDTO>>(null, false, "type not found");
            }
            context.ProductTypes.Remove(type);
            await context.SaveChangesAsync();

            var types = await context.Products.Include(p => p.ProductType).ToListAsync();

            var productDTOs = types.Select(t => mapper.Map<GetProductTypeDTO>(t)).ToList();
            return new ResponseInfo<List<GetProductTypeDTO>>(productDTOs, true, "all types");
        }
    }
}
