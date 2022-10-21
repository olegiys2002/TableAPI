using Microsoft.AspNetCore.Http;

namespace Shared
{
    public static class Image
    {
        public static byte[] ImageInBytes(IFormFile image)
        {
            byte[] imageData = null;

            using (var binaryWriter = new BinaryReader(image.OpenReadStream()))
            {
                imageData = binaryWriter.ReadBytes((int)image.Length);
            }
            return imageData;

        }
    }
}
