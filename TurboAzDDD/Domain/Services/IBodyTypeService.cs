using System;
using Domain.DTOs.BodyType;

namespace Domain.Services
{
	public interface IBodyTypeService 
	{
        Task<bool> CreateAsync(CreateBodyTypeDto createBodyTypeDto);
        Task<bool> UpdateAsync(int id, UpdateBodyTypeDto updateBodyTypeDto);
        Task<List<GetBodyTypeDto>> GetAllAsync();
        Task<GetBodyTypeDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

