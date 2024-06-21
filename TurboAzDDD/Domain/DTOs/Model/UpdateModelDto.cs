using System;
namespace Domain.DTOs.Model
{
	public class UpdateModelDto
	{
        public string ModelName { get; set; } = null!;
        public int BrandId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

