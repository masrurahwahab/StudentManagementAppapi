namespace StudentManagementAppapi.PasswordValidation
{    
    public interface IPasswordHashing
    {
        string HashPassword(string password, byte[] salt);
        byte[] GenerateSalt();
        bool VerifyPassword(string inputPassword, string storedHash, byte[] storedSalt);
    }
   
}
