using AutoMapper;
using Domain.DTOs.BodyType;
using Domain.DTOs.Brand;
using Domain.DTOs.Color;
using Domain.DTOs.FuelType;
using Domain.DTOs.Transmission;
using Domain.Entities;

namespace Infrastructure.Mapper
{
    public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateBrandDto, Brand>()
				.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
				.ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
			CreateMap<UpdateBrandDto, Brand>();
			CreateMap<Brand, GetBrandDto>();

            CreateMap<CreateColorDto, Color>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateColorDto, Color>();
            CreateMap<Color, GetColorDto>();

            CreateMap<CreateFuelTypeDto, FuelType>()
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateFuelTypeDto, FuelType>();
            CreateMap<FuelType, GetFuelTypeDto>();

            CreateMap<CreateTransmissionDto, Transmission>()
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateTransmissionDto, Transmission>();
            CreateMap<Transmission, GetTransmissionDto>();

            CreateMap<CreateBodyTypeDto, BodyType>()
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateBodyTypeDto, BodyType>();
            CreateMap<BodyType, GetBodyTypeDto>();

        }
	}
}