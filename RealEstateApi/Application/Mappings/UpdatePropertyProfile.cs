using AutoMapper;
using RealEstateApi.Application.DTOs;
using RealEstateApi.Application.DTOs.RealEstateApi.Application.DTOs;
using RealEstateApi.Domain.Entities;

namespace RealEstateApi.Application.Mappings
{
    public class UpdatePropertyProfile : Profile
    {
            public UpdatePropertyProfile()
            {
                // DTO → Entidad (solo actualiza si el campo tiene valor)
                CreateMap<UpdatePropertyDto, Property>()
                    .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
                    .ForMember(dest => dest.Address, opt => opt.Condition(src => src.Address != null))
                    .ForMember(dest => dest.Price, opt => opt.Condition(src => src.Price.HasValue))
                    .ForMember(dest => dest.CodeInternal, opt => opt.Condition(src => src.CodeInternal != null))
                    .ForMember(dest => dest.Year, opt => opt.Condition(src => src.Year.HasValue))
                    .ForMember(dest => dest.IdOwner, opt => opt.Condition(src => src.IdOwner.HasValue))
                    .ForMember(dest => dest.Images, opt => opt.Ignore())
                    .ForMember(dest => dest.Traces, opt => opt.Ignore())
                    .ForMember(dest => dest.IdProperty, opt => opt.Ignore());

                // Entidad → DTO (solo para mostrar)
                CreateMap<Property, UpdatePropertyDto>()
                    .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                        src.Images != null
                            ? src.Images.Where(i => i.Enabled).Select(i => i.FilePath).ToList()
                            : new List<string>()));
            }
        }

    

}

