using System;
namespace Domain.DTOs.Transmission
{
	public class UpdateTransmissionDto
	{
        public string TransmissionName { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}

