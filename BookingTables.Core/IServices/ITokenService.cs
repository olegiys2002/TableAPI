using Models.Models;


namespace Core.IServices
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
