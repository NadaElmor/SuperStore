using AutoMapper;
using Store.Core.Entities;
using SuperStore.Core.Entities;
using SuperStore.Core.Entities.Order_Aggregate;
using SuperStore.DTOs;

namespace SuperStore.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTO>().ForMember(p=>p.ProductBrand,o=>o.MapFrom(o=>o.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(o => o.ProductType.Name))
                .ForMember(p=>p.PictureUrl,o=>o.MapFrom<ProductPicURLResolver>());
            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDto,BasketItem>();
            CreateMap<AddressDto, Address>();
        }
    }
}
