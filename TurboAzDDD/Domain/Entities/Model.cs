using System;
namespace Domain.Entities
{
	public class Model : BaseEntity
    {
		public string ModelName { get; set; }
		public int BrandId { get; set; }
        public Brand Brand { get; set; }
	}
}

