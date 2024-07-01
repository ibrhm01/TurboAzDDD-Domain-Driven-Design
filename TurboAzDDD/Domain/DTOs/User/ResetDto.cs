using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.User
{
	public class ResetDto
	{
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public string Token { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

    }
}

