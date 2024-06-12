using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Brand
{
    public class CreateBrandDto
	{
        [Required]
        public string BrandName { get; set; } = null!;
	}
}