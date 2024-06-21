using System;
namespace Domain.Entities
{
	public class BodyType : BaseEntity
	{
		public string BodyTypeName { get; set; } = null!;
        public List<Vehicle>? Vehicles { get; set; }

    }
}

