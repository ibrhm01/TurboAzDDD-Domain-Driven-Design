using System;
namespace Application.Exceptions
{
	public class ExistedUserException :Exception
	{

		public ExistedUserException()
		{
		}

        public ExistedUserException(string? message) : base(message)
        {
        }

        public ExistedUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}

