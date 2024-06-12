using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.FuelType
{
	public class CreateFuelTypeDto
	{
        [Required]
        public string FuelTypeName { get; set; } = null!;
	}
}

