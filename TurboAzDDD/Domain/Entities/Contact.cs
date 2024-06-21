using System;
namespace Domain.Entities
{
	public class Contact : BaseEntity
    {
		public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int VehicleId { get; set; }
    }
}

