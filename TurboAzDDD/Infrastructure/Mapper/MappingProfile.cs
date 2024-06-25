using AutoMapper;
using Domain.DTOs.BodyType;
using Domain.DTOs.Brand;
using Domain.DTOs.Color;
using Domain.DTOs.DriveType;
using Domain.DTOs.FuelType;
using Domain.DTOs.Image;
using Domain.DTOs.Market;
using Domain.DTOs.Model;
using Domain.DTOs.Salon;
using Domain.DTOs.Tag;
using Domain.DTOs.Transmission;
using Domain.DTOs.User;
using Domain.DTOs.Vehicle;
using Domain.Entities;

namespace Infrastructure.Mapper
{
    public class MappingProfile:Profile
	{
		public MappingProfile()
		{
            CreateMap<CreateVehicleDto, Vehicle>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateVehicleDto, Vehicle>();
            CreateMap<Vehicle, GetVehicleDto>();

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

            CreateMap<CreateSalonDto, Salon>()
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateSalonDto, Salon>();
            CreateMap<Salon, GetSalonDto>();

            CreateMap<CreateMarketDto, Market>()
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateMarketDto, Market>();
            CreateMap<Market, GetMarketDto>();

            CreateMap<CreateDriveTypeDto, Domain.Entities.DriveType>()
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateDriveTypeDto, Domain.Entities.DriveType>();
            CreateMap<Domain.Entities.DriveType, GetDriveTypeDto>();

            CreateMap<CreateModelDto, Model>()
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateModelDto, Model>();
            CreateMap<Model, GetModelDto>();

            CreateMap<CreateImageDto, Image>()
              .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
              .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateImageDto, Image>();
            CreateMap<Image, GetImageDto>();

            CreateMap<CreateTagDto, Tag>()
              .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
              .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<UpdateTagDto, Tag>();
            CreateMap<Tag, GetTagDto>();

            CreateMap<RegisterDto, AppUser>();
              //.ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
            CreateMap<AppUser, GetUserDto>();

        }
	}
}