using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Tag
{
	public class CreateTagDto
	{
        [Required]
        public string TagName { get; set; } = null!;
        public List<int>? VehicleIds { get; set; }

    }
}

