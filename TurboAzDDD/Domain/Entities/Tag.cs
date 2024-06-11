using System;
namespace Domain.Entities
{
	public class Tag : BaseEntity
    {
		public string TagName { get; set; }
		public List<TagVehicle> TagVehicles { get; set; }
	}
}

