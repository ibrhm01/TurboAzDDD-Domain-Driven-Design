﻿using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.Transmission;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
	public class TransmissionService :ITransmissionService
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
        public TransmissionService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<bool> CreateAsync(CreateTransmissionDto createDto)
        {
            if (await _unitOfWork.TransmissionRepository.IsExistAsync(b => b.TransmissionName.Trim() == createDto.TransmissionName.Trim()))
                throw new DuplicateNameException("There is already a Transmission with this name");

            var mapped = _mapper.Map<Transmission>(createDto);

            await _unitOfWork.TransmissionRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync() > 0;

        }

        public async Task<bool> UpdateAsync(int id, UpdateTransmissionDto updateDto)
        {
            var transmission = await _unitOfWork.TransmissionRepository.GetByIdAsyncForAll(id);


            if (transmission is null) throw new EntityNotFoundException("There is no such Transmission");

            else if (await _unitOfWork.TransmissionRepository.IsExistAsync(b => b.TransmissionName.Trim() == updateDto.TransmissionName.Trim()))
                throw new DuplicateNameException("There is already a Transmission with this name");

            else
            {
                var mapped = _mapper.Map<Transmission>(updateDto);
                await _unitOfWork.TransmissionRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync() > 0;
            }

        }

        public async Task<List<GetTransmissionDto>> GetAllAsync()
        {

            var transmissions = await _unitOfWork.TransmissionRepository.GetAllAsync();


            var mapped = _mapper.Map<List<GetTransmissionDto>>(transmissions);
            return mapped;

        }
        public async Task<GetTransmissionDto> GetOneAsync(int id)
        {

            var transmission = await _unitOfWork.TransmissionRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Transmission");

            var mapped = _mapper.Map<GetTransmissionDto>(transmission);
            return mapped;

        }

        public async Task<bool> DeleteAsync(int id)
        {

            var transmission = await _unitOfWork.TransmissionRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Transmission");
            await _unitOfWork.TransmissionRepository.DeleteAsync(transmission);
            return await _unitOfWork.CompleteAsync() > 0;

        }
    }
}

