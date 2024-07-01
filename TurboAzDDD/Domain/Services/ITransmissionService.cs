using System;
using Domain.DTOs.Transmission;

namespace Domain.Services
{
	public interface ITransmissionService
	{
        Task<bool> CreateAsync(CreateTransmissionDto createTransmissionDto);
        Task<bool> UpdateAsync(int id, UpdateTransmissionDto updateTransmissionDto);
        Task<List<GetTransmissionDto>> GetAllAsync();
        Task<GetTransmissionDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

