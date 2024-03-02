using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Maps the ProductBrand, ProductType, and PictureUrl properties from the Product class to the ProductToReturnDto class
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(
                    d => d.ProductBrand,
                    o => o.MapFrom(s => s.ProductBrand != null ? s.ProductBrand.Name : null)
                )
                .ForMember(
                    d => d.ProductType,
                    o => o.MapFrom(s => s.ProductType != null ? s.ProductType.Name : null)
                )
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
