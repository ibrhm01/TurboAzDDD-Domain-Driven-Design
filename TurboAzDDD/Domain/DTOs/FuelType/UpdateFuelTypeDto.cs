using System;
namespace Domain.DTOs.FuelType
{
	public class UpdateFuelTypeDto
    {
        public string FuelTypeName { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;

    }
}

