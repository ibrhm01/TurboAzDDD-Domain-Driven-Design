using System;
using Domain.DTOs.Image;

namespace Domain.Services
{
	public interface IImageService
	{
        Task<int> CreateAsync(CreateImageDto createImageDto, string WebRootPath);
        Task<int> UpdateAsync(int id, UpdateImageDto updateImageDto, string WebRootPath);
        Task<IEnumerable<GetImageDto>> GetAllAsync();
        Task<GetImageDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id, string WebRootPath);
    }
}

