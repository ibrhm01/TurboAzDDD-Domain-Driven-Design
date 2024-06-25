using System;
using Domain.DTOs.DriveType;

namespace Domain.Services
{
	public interface IDriveTypeService
	{
        Task<int> CreateAsync(CreateDriveTypeDto createDriveTypeDto);
        Task<int> UpdateAsync(int id, UpdateDriveTypeDto updateDriveTypeDto);
        Task<List<GetDriveTypeDto>> GetAllAsync();
        Task<GetDriveTypeDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

