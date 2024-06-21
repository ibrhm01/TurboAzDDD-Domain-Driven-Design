using System;
namespace Domain.Entities
{
	public class DriveType : BaseEntity
    {
		public string DriveTypeName { get; set; } = null!;
        public List<Vehicle>? Vehicles { get; set; }
    }
}

