using System;
using Domain.DTOs.Brand;

namespace Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
	public interface IBrandService
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(CreateBrandDto createBrandDto);
        Task<bool> UpdateAsync(int id, UpdateBrandDto updateBrandDto);
        Task<List<GetBrandDto>> GetAllAsync();
        Task<GetBrandDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);

    }
}

