using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.User
{
	public class ForgetDto
	{
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
	}
}

