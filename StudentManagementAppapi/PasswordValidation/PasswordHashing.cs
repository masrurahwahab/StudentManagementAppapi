using System.Security.Cryptography;
using System.Text;

namespace StudentManagementAppapi.PasswordValidation
{   
    public class PasswordHashing : IPasswordHashing
    {
        public string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combined = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));
                var hash = sha256.ComputeHash(combined);
                return Convert.ToHexString(hash);
            }
        }

        public byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(16);
        }

        public bool VerifyPassword(string inputPassword, string storedHash, byte[] storedSalt)
        {
            string hashedInput = HashPassword(inputPassword, storedSalt);
            return string.Equals(hashedInput, storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
   
}
