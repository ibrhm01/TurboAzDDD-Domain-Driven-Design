using System;
namespace Application.Exceptions
{
	public class FileSizeException : Exception
	{
		public FileSizeException()
		{
		}

        public FileSizeException(string? message) : base(message)
        {
        }

        public FileSizeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

