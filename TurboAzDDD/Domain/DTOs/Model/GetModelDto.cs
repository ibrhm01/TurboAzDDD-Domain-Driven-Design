using System;
namespace Domain.DTOs.Model
{
	public class GetModelDto
	{
        public string ModelName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int BrandId { get; set; }
        public DateTime CreatedDate { get; set; }
        //public IEnumerable<Entities.Vehicle> Vehicles { get; set; } = null!;
    }
}

