using Microsoft.AspNetCore.Http;

namespace Application.Extensions
{
	public static class ImageExtension
	{
		public static async Task<string> SaveImageFileAsync(this IFormFile file, string web, string root)
		{
			string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
			string filePath = Path.Combine(web, root, fileName);

			using(FileStream stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return fileName;

        }

        public static void DeleteImageFile(this IFormFile file, string web, string root, string fileName)
        {
            string filePath = Path.Combine(web, root, fileName);

			if (File.Exists(filePath))
			{
				File.Delete(filePath);
            }
        }

        public static bool ImageTypeChecker(this IFormFile file)
        {
			return file.ContentType.Contains("image");
        }

        public static bool ImageSizeChecker(this IFormFile file, int maxSize)
        {
			return file.Length / 1024 <= maxSize;
	    }
    }
}