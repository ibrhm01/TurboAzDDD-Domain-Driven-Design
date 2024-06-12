using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Brand
{
	public class UpdateBrandDto
	{
        [Required]
        public string BrandName { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}

