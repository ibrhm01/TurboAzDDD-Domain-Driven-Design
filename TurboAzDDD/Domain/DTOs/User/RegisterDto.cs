using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Domain.DTOs.User
{
	public class RegisterDto
	{
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string RePassword { get; set; } = null!;
       
    }
}

