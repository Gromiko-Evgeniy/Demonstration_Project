
namespace OnlineStore
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddUpdateProductDTO, Product>();
            //CreateMap<Product, AddUpdateProductDTO>();
            CreateMap<Product, GetProductDTO>();
            
            CreateMap<AddOrderDTO, Order>();
            //CreateMap<Order, AddOrderDTO>();
            CreateMap<Order, GetOrderDTO>();

            CreateMap<AddUpdateProductTypeDTO, ProductType>();
            //CreateMap<ProductType, AddUpdateProductTypeDTO>();
            CreateMap<ProductType, GetProductTypeDTO>();
        }
    }
}
