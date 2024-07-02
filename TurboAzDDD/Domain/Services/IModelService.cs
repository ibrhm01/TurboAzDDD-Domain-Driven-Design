using System;
using Domain.DTOs.Model;

namespace Domain.Services
{
	public interface IModelService
	{
        Task<bool> CreateAsync(CreateModelDto createModelDto);
        Task<bool> UpdateAsync(int id, UpdateModelDto updateModelDto);
        Task<List<GetModelDto>> GetAllAsync();
        Task<GetModelDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

