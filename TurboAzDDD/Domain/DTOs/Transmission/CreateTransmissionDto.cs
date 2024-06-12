using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Transmission
{
	public class CreateTransmissionDto
	{
        [Required]
        public string TransmissionName { get; set; } = null!;
        
    }
}

