using OnlineStore.DTOs.Product;

namespace OnlineStore.Services.ProductService
{
    public interface IProductService
    {
        Task<ResponseInfo<List<GetProductDTO>>> GetAllProducts();
        Task<ResponseInfo<GetProductDTO>> GetSingleProduct(int id);
        Task<ResponseInfo<List<GetProductDTO>>> AddProduct(AddUpdateProductDTO product);
        Task<ResponseInfo<GetProductDTO>> UpdateProduct(int id, AddUpdateProductDTO product);
        Task<ResponseInfo<List<GetProductDTO>>> DeleteProduct(int id);
    }
}
