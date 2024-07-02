using Application.Exceptions;
using AutoMapper;
using Domain;
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
        public async Task<bool> CreateAsync(CreateColorDto createDto)
        {
            if (await _unitOfWork.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == createDto.ColorName.Trim()))
                throw new DuplicateNameException("There is already a Color with this name");

            var mapped = _mapper.Map<Color>(createDto);

            await _unitOfWork.ColorRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync() > 0;

        }

        public async Task<bool> UpdateAsync(int id, UpdateColorDto updateDto)
        {
            var color = await _unitOfWork.ColorRepository.GetByIdAsyncForAll(id);


            if (color is null) throw new EntityNotFoundException("There is no such Color");

            else if (await _unitOfWork.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == updateDto.ColorName.Trim()))
                throw new DuplicateNameException("There is already a Color with this name");

            else
            {
                // var mapped = _mapper.Map<Color>(updateDto);
                var mapped = _mapper.Map<Color>(updateDto);

                await _unitOfWork.ColorRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync() > 0;
            }

        }

        public async Task<List<GetColorDto>> GetAllAsync()
        {
            var colors = await _unitOfWork.ColorRepository.GetAllAsync();

            var mapped = _mapper.Map<List<GetColorDto>>(colors);
            return mapped;

        }
        public async Task<GetColorDto> GetOneAsync(int id)
        {

            var color = await _unitOfWork.ColorRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Color");

            var mapped = _mapper.Map<GetColorDto>(color);
            return mapped;

        }

        public async Task<bool> DeleteAsync(int id)
        {

            var color = await _unitOfWork.ColorRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Color");
            await _unitOfWork.ColorRepository.DeleteAsync(color);
            return await _unitOfWork.CompleteAsync() > 0;

        }
    }
}

