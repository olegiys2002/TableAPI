using System.Security.Cryptography;
using System.Text;

namespace SharedAssembly
{
    public static class Hash
    {
        public static string HashPassword(string password)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedBytes = SHA256.HashData(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}