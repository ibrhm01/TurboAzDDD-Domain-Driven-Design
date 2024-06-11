using System;
namespace Domain.DTOs.Brand
{
	public class GetBrandDto 
	{
        public string BrandName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}

