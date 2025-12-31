using Microsoft.AspNetCore.Http;

namespace E_commerce.Core.Services
{
    public interface IImageServices
    {
        Task<List<string>> UploadImageAsync(IFormFileCollection File, string src);

        void DeleteImage(string src);
    }
}
