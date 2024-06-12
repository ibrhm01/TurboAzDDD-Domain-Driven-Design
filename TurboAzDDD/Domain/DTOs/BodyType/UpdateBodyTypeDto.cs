using System;
namespace Domain.DTOs.BodyType
{
	public class UpdateBodyTypeDto
	{
        public string BodyTypeName { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}

