using System;
namespace Domain.Entities
{
	public class DriveType : BaseEntity
    {
		public string DriveTypeName { get; set; }
        public List<Vehicle> Vehicles { get; set; }
	}
}

