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
        Task<int> CreateAsync(CreateBrandDto createBrandDto);
        Task<int> UpdateAsync(int id, UpdateBrandDto updateBrandDto);
        Task<List<GetBrandDto>> GetAllAsync();
        Task<GetBrandDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);

    }
}

