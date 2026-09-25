using APL.DTOs;

namespace APL.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserDTO user);
    }
}