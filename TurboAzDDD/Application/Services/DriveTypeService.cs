using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.DTOs.DriveType;
using Domain.Exceptions;
using Domain.Services;
using DriveType = Domain.Entities.DriveType;


namespace Application.Services
{
    public class DriveTypeService : IDriveTypeService
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
        public DriveTypeService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<bool> CreateAsync(CreateDriveTypeDto createDto)
        {
            if (await _unitOfWork.DriveTypeRepository.IsExistAsync(b => b.DriveTypeName.Trim() == createDto.DriveTypeName.Trim()))
                throw new DuplicateNameException("There is already a DriveType with this name");

            var mapped = _mapper.Map<DriveType>(createDto);

            await _unitOfWork.DriveTypeRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync() > 0;

        }

        public async Task<bool> UpdateAsync(int id, UpdateDriveTypeDto updateDto)
        {
            var driveType = await _unitOfWork.DriveTypeRepository.GetByIdAsyncForAll(id);


            if (driveType is null) throw new EntityNotFoundException("There is no such DriveType");

            else if (await _unitOfWork.DriveTypeRepository.IsExistAsync(b => b.DriveTypeName.Trim() == updateDto.DriveTypeName.Trim()))
                throw new DuplicateNameException("There is already a DriveType with this name");

            else
            {
                var mapped = _mapper.Map<DriveType>(updateDto);
                await _unitOfWork.DriveTypeRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync() > 0;
            }

        }

        public async Task<List<GetDriveTypeDto>> GetAllAsync()
        {
            var driveTypes = await _unitOfWork.DriveTypeRepository.GetAllAsync();

            var mapped = _mapper.Map<List<GetDriveTypeDto>>(driveTypes);
            return mapped;

        }
        public async Task<GetDriveTypeDto> GetOneAsync(int id)
        {

            var driveType = await _unitOfWork.DriveTypeRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such DriveType");

            var mapped = _mapper.Map<GetDriveTypeDto>(driveType);
            return mapped;

        }

        public async Task<bool> DeleteAsync(int id)
        {

            var driveType = await _unitOfWork.DriveTypeRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such DriveType");
            await _unitOfWork.DriveTypeRepository.DeleteAsync(driveType);
            return await _unitOfWork.CompleteAsync() > 0;

        }
    }
}

