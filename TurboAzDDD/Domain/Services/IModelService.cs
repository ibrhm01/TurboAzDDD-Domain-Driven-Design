using System;
using Domain.DTOs.Model;

namespace Domain.Services
{
	public interface IModelService
	{
        Task<int> CreateAsync(CreateModelDto createModelDto);
        Task<int> UpdateAsync(int id, UpdateModelDto updateModelDto);
        Task<List<GetModelDto>> GetAllAsync();
        Task<GetModelDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

