using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.FuelType;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
	public class FuelTypeService :IFuelTypeService
	{

        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public FuelTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        /// 
        public async Task<bool> CreateAsync(CreateFuelTypeDto createDto)
        {
            if (await _unitOfWork.FuelTypeRepository.IsExistAsync(b => b.FuelTypeName.Trim() == createDto.FuelTypeName.Trim()))
                throw new DuplicateNameException("There is already a FuelType with this name");

            var mapped = _mapper.Map<FuelType>(createDto);

            await _unitOfWork.FuelTypeRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync() > 0;

        }

        public async Task<bool> UpdateAsync(int id, UpdateFuelTypeDto updateDto)
        {
            var fuelType = await _unitOfWork.FuelTypeRepository.GetByIdAsyncForAll(id);


            if (fuelType is null) throw new EntityNotFoundException("There is no such FuelType");

            else if (await _unitOfWork.FuelTypeRepository.IsExistAsync(b => b.FuelTypeName.Trim() == updateDto.FuelTypeName.Trim()))
                throw new DuplicateNameException("There is already a FuelType with this name");

            else
            {
                var mapped = _mapper.Map<FuelType>(updateDto);
                await _unitOfWork.FuelTypeRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync() > 0;
            }

        }

        public async Task<List<GetFuelTypeDto>> GetAllAsync()
        {
            var fuelTypes = await _unitOfWork.FuelTypeRepository.GetAllAsync();

            var mapped = _mapper.Map<List<GetFuelTypeDto>>(fuelTypes);
            return mapped;

        }
        public async Task<GetFuelTypeDto> GetOneAsync(int id)
        {
            var fuelType= await _unitOfWork.FuelTypeRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such FuelType");

            var mapped = _mapper.Map<GetFuelTypeDto>(fuelType);
            return mapped;

        }

        public async Task<bool> DeleteAsync(int id)
        {

            var fuelType = await _unitOfWork.FuelTypeRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such FuelType");
            await _unitOfWork.FuelTypeRepository.DeleteAsync(fuelType);
            return await _unitOfWork.CompleteAsync() > 0;

        }
    }
}

