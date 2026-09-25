using APL.DTOs;
using Microsoft.AspNetCore.Identity;

namespace APL.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        Task<PasswordVerificationResult> VerifyHashedPasswordAsync(UserDTO user, string providedPassword);
    }
}
