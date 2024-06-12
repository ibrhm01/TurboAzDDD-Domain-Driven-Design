using System;
namespace Domain.DTOs.Model
{
	public class UpdateModelDto
	{
        public string ModelName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int BrandId { get; set; }
    }
}

