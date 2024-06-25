using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.User
{
	public class GetUserDto
	{
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

