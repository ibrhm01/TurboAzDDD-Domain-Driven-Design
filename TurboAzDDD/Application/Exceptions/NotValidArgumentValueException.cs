using System;
using Microsoft.AspNetCore.Identity;

namespace Application.Exceptions
{
	public class NotValidArgumentValueException :Exception
	{
		public NotValidArgumentValueException()
		{
		}

        public NotValidArgumentValueException(string? message) : base(message)
        {
        }
        
        public NotValidArgumentValueException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

