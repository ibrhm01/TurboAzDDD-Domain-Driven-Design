using System.Linq;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.Image;
using Domain.DTOs.Vehicle;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class VehicleService : IVehicleService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        /// 
        public async Task<bool> CreateAsync(CreateVehicleDto createDto)
        {
            if (!await _unitOfWork.BodyTypeRepository.IsExistAsync(v => v.Id == createDto.BodyTypeId))
                throw new EntityNotFoundException("There is no such BodyType with this ID");
            if (!await _unitOfWork.DriveTypeRepository.IsExistAsync(v => v.Id == createDto.DriveTypeId))
                throw new EntityNotFoundException("There is no such DriveType with this ID");
            if (!await _unitOfWork.FuelTypeRepository.IsExistAsync(v => v.Id == createDto.FuelTypeId))
                throw new EntityNotFoundException("There is no such FuelType with this ID");

            if(createDto.MarketId is not null)
            {
                if (!await _unitOfWork.MarketRepository.IsExistAsync(v => v.Id == createDto.MarketId))
                    throw new EntityNotFoundException("There is no such Market with this ID");
            }
           
            if (!await _unitOfWork.ModelRepository.IsExistAsync(v => v.Id == createDto.ModelId && v.BrandId == createDto.BrandId))
                throw new EntityNotFoundException("Either there is no such Model or the model you have provided does not have any relation with the brand");
            if (!await _unitOfWork.BrandRepository.IsExistAsync(v => v.Id == createDto.BrandId))
                throw new EntityNotFoundException("There is no such Brand with this ID");
            if (createDto.TransmissionId is not null)
            {
                if (!await _unitOfWork.TransmissionRepository.IsExistAsync(v => v.Id == createDto.TransmissionId))
                    throw new EntityNotFoundException("There is no such Transmission with this ID");
            }
            if (!await _unitOfWork.ColorRepository.IsExistAsync(v => v.Id == createDto.ColorId))
                throw new EntityNotFoundException("There is no such Color with this ID");

            var mapped = _mapper.Map<Vehicle>(createDto);
            
            await _unitOfWork.VehicleRepository.CreateAsync(mapped);
            var result= await _unitOfWork.CompleteAsync() > 0;
            if (createDto.TagIds is not null)
            {
                foreach (var tagId in createDto.TagIds)
                {
                    TagVehicle tagVehicle = new();
                    tagVehicle.VehicleId = mapped.Id;
                    tagVehicle.TagId = tagId;
                    mapped.TagVehicles.Add(tagVehicle);
                }

                await _unitOfWork.CompleteAsync();
            }

            if (createDto.Photos is not null)
            {
                foreach (var photo in createDto.Photos)
                {
                    CreateImageDto createImageDto = new();
                    createImageDto.VehicleId = mapped.Id;
                    createImageDto.Photo = photo;
                    await _imageService.CreateAsync(createImageDto);
                }
            }

            return result;

        }

        public async Task<bool> UpdateAsync(int id, UpdateVehicleDto updateDto)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsyncForAll(id);
            if (vehicle is null) throw new EntityNotFoundException("There is no such Vehicle");


            if (!await _unitOfWork.BodyTypeRepository.IsExistAsync(v => v.Id == updateDto.BodyTypeId))
                throw new EntityNotFoundException("There is no such BodyType with this ID");
            if (!await _unitOfWork.DriveTypeRepository.IsExistAsync(v => v.Id == updateDto.DriveTypeId))
                throw new EntityNotFoundException("There is no such DriveType with this ID");
            if (!await _unitOfWork.FuelTypeRepository.IsExistAsync(v => v.Id == updateDto.FuelTypeId))
                throw new EntityNotFoundException("There is no such FuelType with this ID");

            if (updateDto.MarketId is not null)
            {
                if (!await _unitOfWork.MarketRepository.IsExistAsync(v => v.Id == updateDto.MarketId))
                    throw new EntityNotFoundException("There is no such Market with this ID");
            }

            if (!await _unitOfWork.ModelRepository.IsExistAsync(v => v.Id == updateDto.ModelId && v.BrandId == updateDto.BrandId))
                throw new EntityNotFoundException("Either there is no such Model or the model you have provided does not have any relation with the brand");
            if (!await _unitOfWork.BrandRepository.IsExistAsync(v => v.Id == updateDto.BrandId))
                throw new EntityNotFoundException("There is no such Brand with this ID");
            if (updateDto.TransmissionId is not null)
            {
                if (!await _unitOfWork.TransmissionRepository.IsExistAsync(v => v.Id == updateDto.TransmissionId))
                    throw new EntityNotFoundException("There is no such Transmission with this ID");
            }
            if (!await _unitOfWork.ColorRepository.IsExistAsync(v => v.Id == updateDto.ColorId))
                throw new EntityNotFoundException("There is no such Color with this ID");



            var mapped = _mapper.Map(updateDto, vehicle);
          
            await _unitOfWork.VehicleRepository.UpdateAsync(mapped);
            var result = await _unitOfWork.CompleteAsync() > 0;

            if (updateDto.TagIds is not null)
            {
                mapped.TagVehicles = new();
                foreach (var tagId in updateDto.TagIds)
                {
                    TagVehicle tagVehicle = new();
                    tagVehicle.VehicleId = mapped.Id;
                    tagVehicle.TagId = tagId;
                    mapped.TagVehicles.Add(tagVehicle);
                }
                await _unitOfWork.CompleteAsync();
            }
            if (updateDto.Photos is not null)
            {
                foreach (var photo in updateDto.Photos)
                {
                    UpdateImageDto updateImageDto = new();
                    CreateImageDto createImageDto = new();

                    updateImageDto.VehicleId = mapped.Id;
                    updateImageDto.Photo = photo;

                    createImageDto.VehicleId = mapped.Id;
                    createImageDto.Photo = photo;
                    if (vehicle.Images is not null)
                    {
                        if (vehicle.Images.Count > 0)
                        {
                            await _imageService.UpdateAsync(vehicle.Images.FirstOrDefault().Id, updateImageDto);
                        }
                    }
                    else
                    {
                        await _imageService.CreateAsync(createImageDto);
                    }

                }
            }

            return result;
        }

        public async Task<List<GetVehicleDto>> GetAllAsync()
        {
            List<GetVehicleDto> getVehicleDtos = new();

            List<Vehicle> vehicles = await _unitOfWork.VehicleRepository.GetAllAsync();


            var mapped = _mapper.Map(vehicles, getVehicleDtos);
            return mapped;

        }
        public async Task<GetVehicleDto> GetOneAsync(int id)
        {
            GetVehicleDto getVehicleDto = new();

            Vehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Vehicle");

            var mapped = _mapper.Map(vehicle, getVehicleDto);
            return mapped;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Vehicle");
            await _unitOfWork.VehicleRepository.DeleteAsync(vehicle);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public List<GetVehicleDto> GetAllFilteredAsync(GetAllFilteredVehiclesDto getAllFilteredVehiclesDto)
        {
            List<GetVehicleDto> getVehicleDtos = new();
            IQueryable<Vehicle> vehicles =  _unitOfWork.VehicleRepository.GetAllQueryable();

            if (getAllFilteredVehiclesDto.BrandId is not null) vehicles = vehicles.Where(v => v.Model.BrandId == getAllFilteredVehiclesDto.BrandId);
            if (getAllFilteredVehiclesDto.ModelId is not null) vehicles = vehicles.Where(v => v.ModelId == getAllFilteredVehiclesDto.ModelId);
            if (getAllFilteredVehiclesDto.PriceMin is not null) vehicles = vehicles.Where(v => v.Price >= getAllFilteredVehiclesDto.PriceMin);
            if (getAllFilteredVehiclesDto.PriceMax is not null) vehicles = vehicles.Where(v => v.Price <= getAllFilteredVehiclesDto.PriceMax);
            if (getAllFilteredVehiclesDto.IsBarterPossible is not null) vehicles = vehicles.Where(v => v.IsBarterPossible == getAllFilteredVehiclesDto.IsBarterPossible);
            if (getAllFilteredVehiclesDto.WithCredit is not null) vehicles = vehicles.Where(v => v.WithCredit == getAllFilteredVehiclesDto.WithCredit);
            if (getAllFilteredVehiclesDto.BodyTypeId is not null) vehicles = vehicles.Where(v => v.BodyTypeId == getAllFilteredVehiclesDto.BodyTypeId);
            if (getAllFilteredVehiclesDto.ColorId is not null) vehicles = vehicles.Where(v => v.ColorId == getAllFilteredVehiclesDto.ColorId);
            if (getAllFilteredVehiclesDto.FuelTypeId is not null) vehicles = vehicles.Where(v => v.FuelTypeId == getAllFilteredVehiclesDto.FuelTypeId);
            if (getAllFilteredVehiclesDto.DriveTypeId is not null) vehicles = vehicles.Where(v => v.DriveTypeId == getAllFilteredVehiclesDto.DriveTypeId);
            if (getAllFilteredVehiclesDto.TransmissionId is not null) vehicles = vehicles.Where(v => v.TransmissionId == getAllFilteredVehiclesDto.TransmissionId);
            if (getAllFilteredVehiclesDto.PowerOutputMin is not null) vehicles = vehicles.Where(v => v.PowerOutput >= getAllFilteredVehiclesDto.PowerOutputMin);
            if (getAllFilteredVehiclesDto.PowerOutputMax is not null) vehicles = vehicles.Where(v => v.PowerOutput <= getAllFilteredVehiclesDto.PowerOutputMax);
            if (getAllFilteredVehiclesDto.MileageMin is not null) vehicles = vehicles.Where(v => v.PowerOutput >= getAllFilteredVehiclesDto.MileageMin);
            if (getAllFilteredVehiclesDto.MileageMax is not null) vehicles = vehicles.Where(v => v.PowerOutput <= getAllFilteredVehiclesDto.MileageMax);
            if (getAllFilteredVehiclesDto.NumberOfOwners is not null) vehicles = vehicles.Where(v => v.NumberOfOwners <= getAllFilteredVehiclesDto.NumberOfOwners);
            if (getAllFilteredVehiclesDto.NumberOfSeats is not null) vehicles = vehicles.Where(v => v.NumberOfSeats <= getAllFilteredVehiclesDto.NumberOfSeats);
            if (getAllFilteredVehiclesDto.MarketId is not null) vehicles = vehicles.Where(v => v.MarketId <= getAllFilteredVehiclesDto.MarketId);
            if (getAllFilteredVehiclesDto.YearOfManufactureMin is not null) vehicles = vehicles.Where(v => v.YearOfManufacture >= getAllFilteredVehiclesDto.YearOfManufactureMin);
            if (getAllFilteredVehiclesDto.YearOfManufactureMax is not null) vehicles = vehicles.Where(v => v.YearOfManufacture <= getAllFilteredVehiclesDto.YearOfManufactureMax);
            if (getAllFilteredVehiclesDto.IsBroken is not null) vehicles = vehicles.Where(v => v.IsBroken == getAllFilteredVehiclesDto.IsBroken);
            if (getAllFilteredVehiclesDto.IsColored is not null) vehicles = vehicles.Where(v => v.IsColored == getAllFilteredVehiclesDto.IsColored);
            if (getAllFilteredVehiclesDto.IsDamaged is not null) vehicles = vehicles.Where(v => v.IsDamaged == getAllFilteredVehiclesDto.IsDamaged);
            if (getAllFilteredVehiclesDto.EngineDisplacementMin is not null) vehicles = vehicles.Where(v => v.EngineDisplacement >= getAllFilteredVehiclesDto.EngineDisplacementMin);
            if (getAllFilteredVehiclesDto.EngineDisplacementMax is not null) vehicles = vehicles.Where(v => v.EngineDisplacement <= getAllFilteredVehiclesDto.EngineDisplacementMax);
            if (getAllFilteredVehiclesDto.TagIds is not null)
            {
                foreach (var tagId in getAllFilteredVehiclesDto.TagIds)
                {
                    vehicles = vehicles.Where(v => v.TagVehicles != null && v.TagVehicles.Any(tv => tv.TagId == tagId));
                }
            }

            var mapped = _mapper.Map(vehicles.ToList(), getVehicleDtos);

            return mapped;

        }
    }
}

