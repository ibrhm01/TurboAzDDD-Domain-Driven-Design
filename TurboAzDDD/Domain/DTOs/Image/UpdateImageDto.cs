using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Domain.DTOs.Image
{
	public class UpdateImageDto
	{
        [Required]
        public IFormFile Photo { get; set; } = null!;
        [Required]
        public int VehicleId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string? ImageUrl { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}

