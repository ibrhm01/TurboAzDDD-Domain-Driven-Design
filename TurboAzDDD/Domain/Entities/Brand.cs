namespace Domain.Entities
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; } = null!;
        public List<Model>? Models { get; set; } 
    }
}