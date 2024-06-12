using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.DriveType
{
	public class UpdateDriveTypeDto
	{
        [Required]
        public string DriveTypeName { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; }
    }
}

