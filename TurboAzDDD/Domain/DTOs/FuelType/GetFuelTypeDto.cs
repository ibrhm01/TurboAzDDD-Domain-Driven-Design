using System;
namespace Domain.DTOs.FuelType
{
	public class GetFuelTypeDto
    {
        public string FuelTypeName { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        //public IEnumerable<Entities.Vehicle> Vehicles { get; set; } = null!;

    }
}

