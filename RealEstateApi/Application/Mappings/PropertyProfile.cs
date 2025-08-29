using AutoMapper;

using RealEstateApi.Application.DTOs;
using RealEstateApi.Application.DTOs.RealEstateApi.Application.DTOs;
using RealEstateApi.Domain.Entities;

namespace RealEstateApi.Application.Mappings
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            // Property ↔ PropertyDto
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                    src.Images != null
                        ? src.Images.Where(i => i.Enabled).Select(i => i.FilePath).ToList()
                        : new List<string>()))
                .ReverseMap()
                .ForMember(dest => dest.Images, opt => opt.Ignore()) // se manejan aparte
            ;

            // PropertyImage ↔ PropertyImageDto
            CreateMap<PropertyImage, PropertyImageDto>().ReverseMap();
        }
    }
}

