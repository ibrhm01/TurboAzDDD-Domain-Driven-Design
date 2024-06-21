using System;
namespace Domain.DTOs.DriveType
{
	public class GetDriveTypeDto
	{
        public string GetDriveName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        //public IEnumerable<Entities.Vehicle> Vehicles { get; set; } = null!;
    }
}

