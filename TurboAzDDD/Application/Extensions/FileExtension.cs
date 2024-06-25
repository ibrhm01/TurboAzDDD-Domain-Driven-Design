using System;
using Microsoft.AspNetCore.Http;

namespace Application.Extensions
{
	public static class FileExtension
	{
        public static async Task<string> ReadFileAsync(this string body, string path)
        {

            using (StreamReader reader = new StreamReader(path))
            {
                body = await reader.ReadToEndAsync();
            }

            return body;

        }
    }
}

