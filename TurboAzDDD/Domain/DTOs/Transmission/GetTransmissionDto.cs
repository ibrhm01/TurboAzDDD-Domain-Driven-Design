using System;
namespace Domain.DTOs.Transmission
{
	public class GetTransmissionDto
	{
        public string TransmissionName { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
    }
}

