using System;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.Salon;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SalonService : ISalonService
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
        public SalonService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<int> CreateAsync(CreateSalonDto createDto)
        {

            if (await _unitOfWork.SalonRepository.IsExistAsync(b => b.SalonName.Trim() == createDto.SalonName.Trim()))
                throw new DuplicateNameException("There is already a Salon with this name");

            var mapped = _mapper.Map<Salon>(createDto);

            await _unitOfWork.SalonRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync();

        }

        public async Task<int> UpdateAsync(int id, UpdateSalonDto updateDto)
        {
            Salon? salon = await _unitOfWork.SalonRepository.GetByIdAsync(id);


            if (salon is null) throw new EntityNotFoundException("There is no such Salon");

            else if (await _unitOfWork.SalonRepository.IsExistAsync(b => b.SalonName.Trim() == updateDto.SalonName.Trim()))
                throw new DuplicateNameException("There is already a Salon with this name");

            else
            {
                var mapped = _mapper.Map(updateDto, salon);
                await _unitOfWork.SalonRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync();
            }

        }

        public async Task<IEnumerable<GetSalonDto>> GetAllAsync()
        {
            List<GetSalonDto> getSalonDtos = new();

            IEnumerable<Salon> salons = await _unitOfWork.SalonRepository.GetAllAsync();


            var mapped = _mapper.Map(salons, getSalonDtos);
            return mapped;

        }
        public async Task<GetSalonDto> GetOneAsync(int id)
        {
            GetSalonDto getSalonDto = new();

            Salon? salon = await _unitOfWork.SalonRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Salon");

            var mapped = _mapper.Map(salon, getSalonDto);
            return mapped;
        }

        public async Task<int> DeleteAsync(int id)
        {

            var salon = await _unitOfWork.SalonRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Salon");
            await _unitOfWork.SalonRepository.DeleteAsync(salon);
            return await _unitOfWork.CompleteAsync();

        }
    }
}

