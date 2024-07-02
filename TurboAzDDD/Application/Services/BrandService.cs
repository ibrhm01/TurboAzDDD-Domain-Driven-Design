using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.Brand;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class BrandService : IBrandService
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
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<bool> CreateAsync(CreateBrandDto createDto)
        {

            if (await _unitOfWork.BrandRepository.IsExistAsync(b => b.BrandName.Trim() == createDto.BrandName.Trim()))
                throw new DuplicateNameException("There is already a Brand with this name");

            var mapped = _mapper.Map<Brand>(createDto);

            await _unitOfWork.BrandRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync() > 0;

        }

        public async Task<bool> UpdateAsync(int id, UpdateBrandDto updateDto)
        {
            var brand = await _unitOfWork.BrandRepository.GetByIdAsyncForAll(id);


            if (brand is null) throw new EntityNotFoundException("There is no such Brand");

            else if (await _unitOfWork.BrandRepository.IsExistAsync(b => b.BrandName.Trim() == updateDto.BrandName.Trim()))
                throw new DuplicateNameException("There is already a Brand with this name");

            else
            {
                var mapped = _mapper.Map<Brand>(updateDto);
                await _unitOfWork.BrandRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync() > 0;
            }

        }

        public async Task<List<GetBrandDto>> GetAllAsync()
        {

            var brands = await _unitOfWork.BrandRepository.GetAllAsync();


            var mapped = _mapper.Map<List<GetBrandDto>>(brands);
            return mapped;

        }
        public async Task<GetBrandDto> GetOneAsync(int id)
        {

            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Brand");

            var mapped = _mapper.Map<GetBrandDto>(brand);
            return mapped;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Brand");

            await _unitOfWork.BrandRepository.DeleteAsync(brand);

            return await _unitOfWork.CompleteAsync() > 0;

        }
    }
}

