using Microsoft.AspNetCore.Http;

namespace Core.IServices
{
    public interface IStorage
    {
        Task CreateAvatarAsync(IFormFile formFile);
    }
}
