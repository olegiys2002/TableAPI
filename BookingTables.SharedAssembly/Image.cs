using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
