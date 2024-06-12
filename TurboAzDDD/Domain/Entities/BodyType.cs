using System;
namespace Domain.Entities
{
	public class BodyType : BaseEntity
	{
		public string? BodyTypeName { get; set; }
        public List<Vehicle>? Vehicles { get; set; }

    }
}

