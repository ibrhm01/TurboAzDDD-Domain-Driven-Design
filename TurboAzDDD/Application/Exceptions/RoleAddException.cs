using System;
namespace Application.Exceptions
{
	public class RoleAddException: Exception
	{
		public RoleAddException()
		{
		}

        public RoleAddException(string? message) : base(message)
        {
        }

        public RoleAddException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

