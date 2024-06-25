using System;
namespace Domain.DTOs.User
{
	public class LoginDto
	{
        public string UserNameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

