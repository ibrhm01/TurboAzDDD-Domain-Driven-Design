using System;
namespace Domain.DTOs.Tag
{
	public class GetTagDto
	{
        public string TagName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

