using OnlineStore.DTOs.ProductType;

namespace OnlineStore.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        Task<ResponseInfo<List<GetProductTypeDTO>>> GetAllProductTypes();
        Task<ResponseInfo<GetProductTypeDTO>> GetSingleProductType(int id);
        Task<ResponseInfo<List<GetProductTypeDTO>>> AddProductType(AddUpdateProductTypeDTO product);
        Task<ResponseInfo<GetProductTypeDTO>> UpdateProductType(int id, AddUpdateProductTypeDTO product);
        Task<ResponseInfo<List<GetProductTypeDTO>>> DeleteProductType(int id);
    }
}
