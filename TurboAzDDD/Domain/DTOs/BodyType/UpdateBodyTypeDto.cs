using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.BodyType
{
	public class UpdateBodyTypeDto
	{
        [Required]
        public string BodyTypeName { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; }
    }
}

