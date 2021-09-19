using AutoMapper;
using MinAPIDemo.Models.Domain;

namespace MinAPIDemo.Models.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductEntity>()
                .ReverseMap();
        }
    }
}
