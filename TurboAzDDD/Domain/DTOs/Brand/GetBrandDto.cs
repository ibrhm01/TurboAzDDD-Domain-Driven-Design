using Domain.DTOs.Model;

namespace Domain.DTOs.Brand
{
	public class GetBrandDto 
	{
        public string BrandName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        //public IEnumerable<Entities.Vehicle> Vehicles { get; set; } = null!;
        //public IEnumerable<Entities.Model> Models { get; set; } = null!;

    }
}

