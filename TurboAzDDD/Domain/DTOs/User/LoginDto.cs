using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.User
{
	public class LoginDto
	{
        [Required]
        public string UserNameOrEmail { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}

