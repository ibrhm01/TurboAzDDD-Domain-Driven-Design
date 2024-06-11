using System;
namespace Domain.Entities
{
	public class Contact : BaseEntity
    {
		public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public int VehicleId { get; set; }
    }
}

