using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.Model;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{

    /// <summary>
    /// 
    /// </summary>
    public class ModelService : IModelService
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
        public ModelService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<bool> CreateAsync(CreateModelDto createDto)
        {

            if (await _unitOfWork.BrandRepository.IsExistAsync(b => b.Id == createDto.BrandId))
            {
                if (await _unitOfWork.ModelRepository.IsExistAsync(b => b.ModelName.Trim() == createDto.ModelName.Trim()))
                    throw new DuplicateNameException("There is already a Model with this name");

                var mapped = _mapper.Map<Model>(createDto);
              
                await _unitOfWork.ModelRepository.CreateAsync(mapped);

                return await _unitOfWork.CompleteAsync() > 0;

            } 
            else throw new EntityNotFoundException("There is no such Brand with this ID");
        }

        public async Task<bool> UpdateAsync(int id, UpdateModelDto updateDto)
        {
            var model = await _unitOfWork.ModelRepository.GetByIdAsyncForAll(id);

            if (model is null) throw new EntityNotFoundException("There is no such Model");
            
            if (await _unitOfWork.BrandRepository.IsExistAsync(b => b.Id == updateDto.BrandId))
            {
                 if (await _unitOfWork.ModelRepository.IsExistAsync(m => m.ModelName.Trim() == updateDto.ModelName.Trim()))
                    throw new DuplicateNameException("There is already a Model with this name");

                 var mapped = _mapper.Map<Model>(updateDto);
                 await _unitOfWork.ModelRepository.UpdateAsync(mapped);
                 return await _unitOfWork.CompleteAsync() > 0;
            }
            else throw new EntityNotFoundException("There is no such Brand with this ID");
        }

        public async Task<List<GetModelDto>> GetAllAsync()
        {
            var models = await _unitOfWork.ModelRepository.GetAllAsync();

            var mapped = _mapper.Map<List<GetModelDto>>(models);
            return mapped;

        }

        public async Task<GetModelDto> GetOneAsync(int id)
        {
            var model = await _unitOfWork.ModelRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Model");

            var mapped = _mapper.Map<GetModelDto>(model);
            return mapped;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var model = await _unitOfWork.ModelRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Model");
            await _unitOfWork.ModelRepository.DeleteAsync(model);
            return await _unitOfWork.CompleteAsync() > 0;

        }
    }
}

