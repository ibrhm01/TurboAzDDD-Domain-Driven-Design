using System;
namespace Domain.DTOs.Color
{
	public class UpdateColorDto
	{
        public string ColorName { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;

    }
}

