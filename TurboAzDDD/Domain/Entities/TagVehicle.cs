﻿namespace Domain.Entities
{
	public class TagVehicle : BaseEntity
	{
		public int TagId { get; set; }
        public int VehicleId { get; set; }
		public Tag Tag { get; set; } = null!;
        public Vehicle Vehicle { get; set; } = null!;
    }
}

