using AccruentInventoryControl.Domain.ApiModels.Response;
using AccruentInventoryControl.Domain.Entities;
using AutoMapper;

namespace AccruentInventoryControl.Application.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));
        }
    }
}
