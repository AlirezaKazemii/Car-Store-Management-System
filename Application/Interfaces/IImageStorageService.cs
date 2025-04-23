using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IImageStorageService
    {
        Task<List<string>> SaveImageAsync(List<IFormFile> images);
    }
}