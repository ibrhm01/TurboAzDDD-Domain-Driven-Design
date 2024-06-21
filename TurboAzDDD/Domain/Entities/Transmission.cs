using System;
namespace Domain.Entities
{
	public class Transmission : BaseEntity
    {
		public string TransmissionName { get; set; } = null!;
        public List<Vehicle>? Vehicles { get; set; } 
    }
}

