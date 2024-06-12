using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.BodyType
{
	public class CreateBodyTypeDto
	{
        [Required]
        public string BodyTypeName { get; set; } = null!;
       
    }
}

