using Microsoft.AspNetCore.Http;

namespace Vanilla.Shared.Extensions
{
    public static class UploadExtension
    {
        const string path = "Uploads";

        public static string SaveLocal(this IFormFile file)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string filePath = GetPath(file);

            using (FileStream stream = new(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                file.CopyTo(stream);

                stream.Dispose();
                stream.Close();
            }

            return filePath;
        }

        public static void DeleteLocal(this IFormFile file)
        {
            string filePath = GetPath(file);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        static string GetPath(IFormFile file)
        {
            return Path.Combine(path, $"{Guid.NewGuid()}.{file.GetFileExtension()}");
        }

        static string? GetFileExtension(this IFormFile file)
        {
            return file.FileName.Split('.').LastOrDefault();
        }
    }
}
