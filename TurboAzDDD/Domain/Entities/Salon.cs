namespace Domain.Entities
{
	public class Salon: BaseEntity
	{
		public string? SalonName { get; set; }
        public string? SalonDescription { get; set; }
        public string? Address { get; set; }
		public List<Vehicle>? Vehicles { get; set; }
	}
}

