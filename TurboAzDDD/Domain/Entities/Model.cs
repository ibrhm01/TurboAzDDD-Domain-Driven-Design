namespace Domain.Entities
{
	public class Model : BaseEntity
    {
		public string ModelName { get; set; } = null!;
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
        public List<Vehicle>? Vehicles { get; set; } 
    }
}

