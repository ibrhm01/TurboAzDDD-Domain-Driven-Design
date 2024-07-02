using System;
using Domain.DTOs.DriveType;

namespace Domain.Services
{
	public interface IDriveTypeService
	{
        Task<bool> CreateAsync(CreateDriveTypeDto createDriveTypeDto);
        Task<bool> UpdateAsync(int id, UpdateDriveTypeDto updateDriveTypeDto);
        Task<List<GetDriveTypeDto>> GetAllAsync();
        Task<GetDriveTypeDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

