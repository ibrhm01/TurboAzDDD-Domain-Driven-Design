using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.BodyType;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
        public class BodyTypeService : IBodyTypeService
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
            public BodyTypeService(IUnitOfWork unitOfWork, IMapper mapper)
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
            public async Task<bool> CreateAsync(CreateBodyTypeDto createDto)
            {

                if (await _unitOfWork.BodyTypeRepository.IsExistAsync(b => b.BodyTypeName.Trim() == createDto.BodyTypeName.Trim()))
                    throw new DuplicateNameException("There is already a BodyType with this name");

                var mapped = _mapper.Map<BodyType>(createDto);

                await _unitOfWork.BodyTypeRepository.CreateAsync(mapped);

                return await _unitOfWork.CompleteAsync() > 0;

            }

            public async Task<bool> UpdateAsync(int id, UpdateBodyTypeDto updateDto)
            {
                var bodyType = await _unitOfWork.BodyTypeRepository.GetByIdAsyncForAll(id);


                if (bodyType is null) throw new EntityNotFoundException("There is no such BodyType");

                else if (await _unitOfWork.BodyTypeRepository.IsExistAsync(b => b.BodyTypeName.Trim() == updateDto.BodyTypeName.Trim()))
                    throw new DuplicateNameException("There is already a BodyType with this name");

                else
                {
                    var mapped = _mapper.Map<BodyType>(updateDto);
                    await _unitOfWork.BodyTypeRepository.UpdateAsync(mapped);
                    return await _unitOfWork.CompleteAsync() > 0;
                }

            }

            public async Task<List<GetBodyTypeDto>> GetAllAsync()
            {
                var bodyTypes = await _unitOfWork.BodyTypeRepository.GetAllAsync();

                var mapped = _mapper.Map<List<GetBodyTypeDto>>(bodyTypes);
                return mapped;

            }
            public async Task<GetBodyTypeDto> GetOneAsync(int id)
            {

                var bodyType = await _unitOfWork.BodyTypeRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such BodyType");

                var mapped = _mapper.Map<GetBodyTypeDto>(bodyType);
                return mapped;
            }

            public async Task<bool> DeleteAsync(int id)
            {

                var bodyType = await _unitOfWork.BodyTypeRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such BodyType");
                await _unitOfWork.BodyTypeRepository.DeleteAsync(bodyType);
                return await _unitOfWork.CompleteAsync() > 0 ;

            }
        }
}

