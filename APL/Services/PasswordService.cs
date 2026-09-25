using APL.DTOs;
using APL.Interfaces;
using BLL.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace APL.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        private readonly IUserManager _userManager;

        public PasswordService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public string HashPassword(string password)
        {
            return Convert.ToBase64String(HashPasswordV1(password, _rng));
        }

        public async Task<PasswordVerificationResult> VerifyHashedPasswordAsync(UserDTO user, string providedPassword)
        {
            var HashedPassword = await _userManager.GetUserPassword((int)user.Id!);

            byte[] decodedHashedPassword = Convert.FromBase64String(HashedPassword);

            switch (decodedHashedPassword[0])
            {
                case 0x00:
                    if(VerifyHashedPasswordV1(decodedHashedPassword, providedPassword))
                    {
                        return PasswordVerificationResult.Success;
                    }
                    else
                    {
                        return PasswordVerificationResult.Failed;
                    }
                default: return PasswordVerificationResult.Failed;
            }
        }

        private static byte[] HashPasswordV1(string password, RandomNumberGenerator rng)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA256;
            const int Pbkdf2PrfIterCount = 1000;
            const int Pbkdf2PrfSubkeyLenght = 256 / 8;
            const int saltSize = 128 / 8;


            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2PrfIterCount, Pbkdf2PrfSubkeyLenght);

            var outputBytes = new byte[13 + saltSize + Pbkdf2PrfSubkeyLenght];
            outputBytes[0] = 0x00;
            WriteNetworkByteOrder(outputBytes, 1, (uint)Pbkdf2Prf);
            WriteNetworkByteOrder(outputBytes, 5, (uint)Pbkdf2PrfIterCount);
            WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
            Buffer.BlockCopy(salt, 0, outputBytes, 13, saltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, Pbkdf2PrfSubkeyLenght);
            return outputBytes;
        }

        private static bool VerifyHashedPasswordV1(byte[] hashedPassword, string password)
        {
            var iterCount = default(int);
            try
            {
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
                iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
                int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

                if(saltLength < 128 / 8)
                {
                    return false;
                }
                byte[] salt = new byte[saltLength];
                Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

                int subkeyLength = hashedPassword.Length - 13 - salt.Length;
                if (subkeyLength < 128 / 8)
                {
                    return false;
                }
                byte[] expectedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                byte[] actualSubKey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);
                return ByteArraysEqual(actualSubKey, expectedSubkey);
            }
            catch
            {
                return false;
            }
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }
    }
}
