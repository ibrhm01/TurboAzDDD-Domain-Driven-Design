using System;
namespace Domain.Entities
{
	public class Tag : BaseEntity
    {
		public string TagName { get; set; } = null!;
		public List<TagVehicle>? TagVehicles { get; set; }
    }
}

