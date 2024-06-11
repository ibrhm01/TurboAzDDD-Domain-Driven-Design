using System;
namespace Domain.Entities
{
	public class NumberOfSeat : BaseEntity
    {
		public string NumberOfSeatName { get; set; }
        public List<Vehicle> Vehicles { get; set; }
	}
}

