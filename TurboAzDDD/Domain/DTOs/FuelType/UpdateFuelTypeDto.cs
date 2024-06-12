using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.FuelType
{
	public class UpdateFuelTypeDto
    {
        [Required]
        public string FuelTypeName { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; } = false;

    }
}

