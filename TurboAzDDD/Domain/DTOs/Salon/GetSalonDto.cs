using System;
namespace Domain.DTOs.Salon
{
	public class GetSalonDto
	{
        public string SalonName { get; set; } = null!;
        public string SalonDescription { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        //public IEnumerable<Entities.Vehicle> Vehicles { get; set; } = null!;
    }
}

