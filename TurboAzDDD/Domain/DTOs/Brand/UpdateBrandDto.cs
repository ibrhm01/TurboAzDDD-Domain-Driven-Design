using System;
namespace Domain.DTOs.Brand
{
	public class UpdateBrandDto
	{
		public string BrandName { get; set; } = null!;
		public bool IsDeleted { get; set; } = false;
    }
}

