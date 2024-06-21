namespace Domain.Entities
{
	public class Salon: BaseEntity
	{
		public string SalonName { get; set; } = null!;
        public string SalonDescription { get; set; } = null!;
        public string Address { get; set; } = null!;
        public List<Vehicle>? Vehicles { get; set; } 
    }
}

