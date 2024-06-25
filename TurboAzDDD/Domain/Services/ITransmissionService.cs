using System;
using Domain.DTOs.Transmission;

namespace Domain.Services
{
	public interface ITransmissionService
	{
        Task<int> CreateAsync(CreateTransmissionDto createTransmissionDto);
        Task<int> UpdateAsync(int id, UpdateTransmissionDto updateTransmissionDto);
        Task<List<GetTransmissionDto>> GetAllAsync();
        Task<GetTransmissionDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

