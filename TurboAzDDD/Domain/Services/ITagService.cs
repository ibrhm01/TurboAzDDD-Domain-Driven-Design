using Domain.DTOs.Tag;

namespace Domain.Services
{
	public interface ITagService 
	{
        Task<int> CreateAsync(CreateTagDto createTagDto);
        Task<int> UpdateAsync(int id, UpdateTagDto updateTagDto);
        Task<List<GetTagDto>> GetAllAsync();
        Task<GetTagDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

