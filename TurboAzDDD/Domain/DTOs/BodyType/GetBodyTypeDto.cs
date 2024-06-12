using System;
namespace Domain.DTOs.BodyType
{
	public class GetBodyTypeDto
	{
        public string BodyTypeName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

