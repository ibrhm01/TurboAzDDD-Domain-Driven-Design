namespace Domain.Entities
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; } = null!;
        public List<Vehicle> Vehicles { get; set; } = null!;
        public List<Model> Models { get; set; } = null!;
    }
}