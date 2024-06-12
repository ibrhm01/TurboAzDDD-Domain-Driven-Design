using System;
namespace Domain.Entities
{
	public class FuelType : BaseEntity
    {
		public string? FuelTypeName { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
	}
}

