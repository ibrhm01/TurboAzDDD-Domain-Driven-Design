using System;
namespace Domain.Entities
{
	public class Market : BaseEntity
    {
        public string MarketName { get; set; } = null!;
        public List<Vehicle>? Vehicles { get; set; } 
    }
}

