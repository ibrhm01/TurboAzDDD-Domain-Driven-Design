using System;
using Domain.DTOs.Image;

namespace Domain.Services
{
	public interface IImageService
	{
        Task<int> CreateAsync(CreateImageDto createImageDto);
        Task<int> UpdateAsync(int id, UpdateImageDto updateImageDto);
        Task<List<GetImageDto>> GetAllAsync();
        Task<GetImageDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

