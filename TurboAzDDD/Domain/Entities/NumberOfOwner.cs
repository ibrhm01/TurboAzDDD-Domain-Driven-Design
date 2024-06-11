using System;
namespace Domain.Entities
{
	public class NumberOfOwner : BaseEntity
    {
        public string NumberOfOwnerName { get; set; }
        public List<Vehicle> Vehicles { get; set; }
	}
}

