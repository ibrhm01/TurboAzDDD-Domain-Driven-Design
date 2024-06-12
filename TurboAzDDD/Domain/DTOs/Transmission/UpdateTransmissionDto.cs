using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Transmission
{
	public class UpdateTransmissionDto
	{
        [Required]
        public string TransmissionName { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}

