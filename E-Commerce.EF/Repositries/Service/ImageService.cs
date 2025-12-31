using E_commerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace E_Commerce.EF.Repositries.Service
{
    public class ImageService : IImageServices
    {
        private readonly IFileProvider fileProvider;
        public ImageService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public async Task<List<string>> UploadImageAsync(IFormFileCollection File, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImgDirectory = Path.Combine("wwwroot","Images", src);
            if (!Directory.Exists(ImgDirectory))
            {
                Directory.CreateDirectory(ImgDirectory);
            }

            foreach (var file in File)
            {
                var filePath = Path.Combine(ImgDirectory, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                SaveImageSrc.Add($"/Images/{src}/{file.FileName}");
            }
            return SaveImageSrc;
        }

        public void DeleteImage(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            var rootPath = info.PhysicalPath;
            File.Delete(rootPath);
        }
    }
}
