using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Tag
{
	public class UpdateTagDto
	{
        [Required]
        public string TagName { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; }
        public List<int>? VehicleIds { get; set; }
    }
}

