namespace Domain.Entities
{
	public class Model : BaseEntity
    {
		public string? ModelName { get; set; }
		public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
    }
}

