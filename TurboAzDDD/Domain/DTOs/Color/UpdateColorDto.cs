using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Color
{
	public class UpdateColorDto
	{
        [Required]
        public string ColorName { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; } = false;

    }
}

