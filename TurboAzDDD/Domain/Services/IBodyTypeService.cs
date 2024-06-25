using System;
using Domain.DTOs.BodyType;

namespace Domain.Services
{
	public interface IBodyTypeService 
	{
        Task<int> CreateAsync(CreateBodyTypeDto createBodyTypeDto);
        Task<int> UpdateAsync(int id, UpdateBodyTypeDto updateBodyTypeDto);
        Task<List<GetBodyTypeDto>> GetAllAsync();
        Task<GetBodyTypeDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

