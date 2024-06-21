using System;
namespace Domain.Entities
{
	public class Color : BaseEntity
    {
		public string ColorName { get; set; } = null!;
        public List<Vehicle>? Vehicles { get; set; }
    }
}

