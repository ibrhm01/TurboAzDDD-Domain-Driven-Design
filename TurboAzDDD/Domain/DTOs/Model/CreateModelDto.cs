using System;
namespace Domain.DTOs.Model
{
	public class CreateModelDto
	{
        public string ModelName { get; set; } = null!;
        public string? BrandName { get; set; }

    }
}

