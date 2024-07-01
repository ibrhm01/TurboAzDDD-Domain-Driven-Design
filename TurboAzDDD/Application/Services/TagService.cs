using System.Data;
using AutoMapper;
using Domain;
using Domain.DTOs.Tag;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{

    public class TagService : ITagService
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
        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<int> CreateAsync(CreateTagDto createDto)
        {

            if (await _unitOfWork.TagRepository.IsExistAsync(b => b.TagName.Trim() == createDto.TagName.Trim()))
                throw new DuplicateNameException("There is already a Tag with this name");
            

            var mapped = _mapper.Map<Tag>(createDto);
            if (createDto.VehicleIds is not null)
            {
                foreach (var vehicleId in createDto.VehicleIds)
                {
                    TagVehicle tagVehicle = new();
                    tagVehicle.TagId = mapped.Id;
                    tagVehicle.VehicleId = vehicleId;
                    mapped.TagVehicles.Add(tagVehicle);
                }
            }

            await _unitOfWork.TagRepository.CreateAsync(mapped);

            return await _unitOfWork.CompleteAsync();

        }

        public async Task<int> UpdateAsync(int id, UpdateTagDto updateDto)
        {
            Tag? tag = await _unitOfWork.TagRepository.GetByIdAsyncForAll(id);


            if (tag is null) throw new EntityNotFoundException("There is no such Tag");

            else if (await _unitOfWork.TagRepository.IsExistAsync(b => b.TagName.Trim() == updateDto.TagName.Trim()))
                throw new DuplicateNameException("There is already a Tag with this name");


            var mapped = _mapper.Map(updateDto, tag);
            if (updateDto.VehicleIds is not null)
            {
                mapped.TagVehicles = new();
                foreach (var vehicleId in updateDto.VehicleIds)
                {
                    TagVehicle tagVehicle = new();
                    tagVehicle.TagId = mapped.Id;
                    tagVehicle.VehicleId = vehicleId;
                    mapped.TagVehicles.Add(tagVehicle);
                }
            }

            await _unitOfWork.TagRepository.UpdateAsync(mapped);
            return await _unitOfWork.CompleteAsync();
            

        }

        public async Task<List<GetTagDto>> GetAllAsync()
        {
            List<GetTagDto> getTagDtos = new();

            List<Tag> tags = await _unitOfWork.TagRepository.GetAllAsync();


            var mapped = _mapper.Map(tags, getTagDtos);
            return mapped;

        }
        public async Task<GetTagDto> GetOneAsync(int id)
        {
            GetTagDto getTagDto = new();

            Tag? tag = await _unitOfWork.TagRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Tag");

            var mapped = _mapper.Map(tag, getTagDto);
            return mapped;
        }

        public async Task<int> DeleteAsync(int id)
        {

            var tag = await _unitOfWork.TagRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Tag");
            await _unitOfWork.TagRepository.DeleteAsync(tag);
            return await _unitOfWork.CompleteAsync();

        }
    }

}

