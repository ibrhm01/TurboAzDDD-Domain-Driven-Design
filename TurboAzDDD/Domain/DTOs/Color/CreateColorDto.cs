using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Color
{
	public class CreateColorDto
	{
        [Required]
        public string ColorName { get; set; } = null!;
    }
}

