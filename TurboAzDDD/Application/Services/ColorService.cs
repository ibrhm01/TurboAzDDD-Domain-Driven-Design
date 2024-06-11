using System;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.Brand;
using Domain.DTOs.Color;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
	public class ColorService :IColorService
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
        public ColorService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<int> CreateAsync(CreateColorDto createDto)
        {
            if (await _unitOfWork.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == createDto.ColorName.Trim()))
                throw new DuplicateNameException("There is already a Color with this name");

            var mapped = _mapper.Map<Color>(createDto);

            await _unitOfWork.ColorRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync();

        }

        public async Task<int> UpdateAsync(int id, UpdateColorDto updateDto)
        {
            Color? color = await _unitOfWork.ColorRepository.GetByIdAsync(id);


            if (color is null) throw new EntityNotFoundException("There is no such Color");

            else if (await _unitOfWork.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == updateDto.ColorName.Trim()))
                throw new DuplicateNameException("There is already a Color with this name");

            else
            {
                var mapped = _mapper.Map(updateDto, color);
                await _unitOfWork.ColorRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync();
            }

        }

        public async Task<IEnumerable<GetColorDto>> GetAllAsync()
        {
            List<GetColorDto> getColorDtos = new();

            IEnumerable<Color> colors = await _unitOfWork.ColorRepository.GetAllAsync();


            var mapped = _mapper.Map(colors, getColorDtos);
            return mapped;

        }
        public async Task<GetColorDto> GetOneAsync(int id)
        {
            GetColorDto getColorDto = new();

            Color? color = await _unitOfWork.ColorRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Color");

            var mapped = _mapper.Map(color, getColorDto);
            return mapped;

        }

        public async Task<int> DeleteAsync(int id)
        {

            var color = await _unitOfWork.ColorRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Color");
            await _unitOfWork.ColorRepository.DeleteAsync(color);
            return await _unitOfWork.CompleteAsync();

        }
    }
}

