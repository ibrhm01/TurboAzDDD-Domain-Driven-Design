using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.DriveType
{
	public class CreateDriveTypeDto
	{
        [Required]
        public string DriveTypeName { get; set; } = null!;
    }
}

