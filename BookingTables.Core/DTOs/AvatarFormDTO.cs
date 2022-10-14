using Microsoft.AspNetCore.Http;
namespace Core.DTOs
{
    public class AvatarFormDTO
    {
        public IFormFile Image { get; set; }
    }
}
