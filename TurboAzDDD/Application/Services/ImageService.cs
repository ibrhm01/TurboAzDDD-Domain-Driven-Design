using System;
using Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Domain;
using Domain.DTOs.Image;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Application.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Services
{
    public class ImageService : IImageService
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
        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<int> CreateAsync(CreateImageDto createDto, string WebRootPath)
        {
            int size = 500;

            if (!createDto.Photo.ImageTypeChecker()) throw new FileIsNotImageException("The type of the file must be Image");
            if (!createDto.Photo.ImageSizeChecker(size)) throw new FileSizeException("The size of the Image cannot be bigger than ${size}");

            if (await _unitOfWork.VehicleRepository.IsExistAsync(v => v.Id == createDto.VehicleId))
            {
                createDto.ImageUrl = await createDto.Photo.SaveImageFileAsync(WebRootPath, "uploads");

                var mapped = _mapper.Map<Image>(createDto);

                await _unitOfWork.ImageRepository.CreateAsync(mapped);

                return await _unitOfWork.CompleteAsync();
            }
            else throw new EntityNotFoundException("There is no such Vehicle with this ID");

        }

        public async Task<int> UpdateAsync(int id, UpdateImageDto updateDto, string WebRootPath)
        {

            Image? image = await _unitOfWork.ImageRepository.GetByIdAsyncForAll(id);

            if (image is null) throw new EntityNotFoundException("There is no such Image");


            int size = 500;

            if (!updateDto.Photo.ImageTypeChecker()) throw new FileIsNotImageException("The type of the file must be Image");
            if (!updateDto.Photo.ImageSizeChecker(size)) throw new FileSizeException("The size of the Image cannot be bigger than ${size}");

            if (await _unitOfWork.VehicleRepository.IsExistAsync(v => v.Id == updateDto.VehicleId))
            {

                updateDto.Photo.DeleteImageFile(WebRootPath, "uploads", image.ImageUrl);

                updateDto.ImageUrl = await updateDto.Photo.SaveImageFileAsync(WebRootPath, "uploads");


                var mapped = _mapper.Map(updateDto, image);
                await _unitOfWork.ImageRepository.UpdateAsync(mapped);
                return await _unitOfWork.CompleteAsync();
            }
            else throw new EntityNotFoundException("There is no such Vehicle with this ID");
        }

        public async Task<IEnumerable<GetImageDto>> GetAllAsync()
        {
            List<GetImageDto> getImageDtos = new();

            IEnumerable<Image> images = await _unitOfWork.ImageRepository.GetAllAsync();


            var mapped = _mapper.Map(images, getImageDtos);
            return mapped;

        }
        public async Task<GetImageDto> GetOneAsync(int id)
        {
            GetImageDto getImageDto = new();

            Image? image = await _unitOfWork.ImageRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Image");

            var mapped = _mapper.Map(image, getImageDto);
            return mapped;

        }

        public async Task<int> DeleteAsync(int id, string WebRootPath)
        {

            var image = await _unitOfWork.ImageRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException("There is no such Image");
            
            await _unitOfWork.ImageRepository.DeleteAsync(image);
            return await _unitOfWork.CompleteAsync();

        }

    }
}

