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
        public async Task<int> CreateAsync(CreateModelDto createDto)
        {

            if (await _unitOfWork.ModelRepository.IsExistAsync(b => b.ModelName.Trim() == createDto.ModelName.Trim()))
                throw new DuplicateNameException("There is already a Model with this name");

            var mapped = _mapper.Map<Model>(createDto);

            await _unitOfWork.ModelRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync();

        }

        public async Task<int> UpdateAsync(int id, UpdateModelDto updateDto)
        {
            Model? model = await _unitOfWork.ModelRepository.GetByIdAsync(id);


            if (model is null) throw new EntityNotFoundException("There is no such Model");

            else if (await _unitOfWork.ModelRepository.IsExistAsync(b => b.ModelName.Trim() == updateDto.ModelName.Trim()))
                throw new DuplicateNameException("There is already a Model with this name");

            else
            {
                var mapped = _mapper.Map(updateDto, model);
                await _unitOfWork.ModelRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync();
            }

        }

        public async Task<IEnumerable<GetModelDto>> GetAllAsync()
        {
            List<GetModelDto> getModelDtos = new();

            IEnumerable<Model> models = await _unitOfWork.ModelRepository.GetAllAsync();


            var mapped = _mapper.Map(models, getModelDtos);
            return mapped;

        }

        public async Task<GetModelDto> GetOneAsync(int id)
        {
            GetModelDto getModelDto = new();

            Model? model = await _unitOfWork.ModelRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Model");

            var mapped = _mapper.Map(model, getModelDto);
            return mapped;
        }

        public async Task<int> DeleteAsync(int id)
        {

            var model = await _unitOfWork.ModelRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Model");
            await _unitOfWork.ModelRepository.DeleteAsync(model);
            return await _unitOfWork.CompleteAsync();

        }
    }
}

