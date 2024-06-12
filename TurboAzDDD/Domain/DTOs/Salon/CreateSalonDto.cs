using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Salon
{
	public class CreateSalonDto
	{
        [Required]
        public string SalonName { get; set; } = null!;
        [Required]
        public string SalonDescription { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
    }
}

