using System;
namespace Domain.Entities
{
	public class Image : BaseEntity
    {
		public string ImageUrl { get; set; } = null!;
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
    }
}

