using System;
namespace Domain.DTOs.Color
{
	public class GetColorDto
	{
        public string ColorName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        //public IEnumerable<Entities.Vehicle> Vehicles { get; set; } = null!;
    }
}

