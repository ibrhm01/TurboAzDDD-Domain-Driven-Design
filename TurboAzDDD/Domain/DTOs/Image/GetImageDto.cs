using System;
namespace Domain.DTOs.Image
{
	public class GetImageDto
	{
		public string ImageUrl { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }

    }
}

