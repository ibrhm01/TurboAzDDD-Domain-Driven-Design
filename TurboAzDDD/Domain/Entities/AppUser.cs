using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class AppUser : IdentityUser
	{
		public string FullName { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

