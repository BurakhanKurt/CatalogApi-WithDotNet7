using AutoMapper;
using Catalog.Entity.DTOs;
using Catalog.Entity.Models;

namespace Catalog.Api.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();    

            CreateMap<Category,CategoryWithProductsDto>();

            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>();

        }
    }
}
