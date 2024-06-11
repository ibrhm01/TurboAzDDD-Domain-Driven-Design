using System;
namespace Domain.Entities
{
	public class Market : BaseEntity
    {
        public string MarketName { get; set; }
        public List<Vehicle> Vehicles { get; set; }
	}
}

