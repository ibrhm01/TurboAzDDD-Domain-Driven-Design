using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Domain.DTOs.Image
{
	public class CreateImageDto
	{
        [Required]
        public IFormFile Photo { get; set; } = null!;
        [Required]
        public int VehicleId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string? ImageUrl { get; set; }
    }
}

