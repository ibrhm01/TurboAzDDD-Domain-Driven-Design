using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Model
{
	public class CreateModelDto
	{
        [Required]
        public string ModelName { get; set; } = null!;
        [Required]
        public int BrandId { get; set; }

    }
}

