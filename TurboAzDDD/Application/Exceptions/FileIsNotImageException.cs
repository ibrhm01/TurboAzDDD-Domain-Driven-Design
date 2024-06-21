using System;
namespace Application.Exceptions
{
	public class FileIsNotImageException :Exception
	{
		public FileIsNotImageException()
		{
		}

        public FileIsNotImageException(string? message) : base(message)
        {
        }

        public FileIsNotImageException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

