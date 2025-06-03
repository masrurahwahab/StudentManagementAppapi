using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.PasswordValidation;
using StudentManagementAppapi.Settings.Interfaces;

namespace StudentManagementAppapi.Settings.Implementation
{
    public class SettingRepository : ISettingRepository
    {
        private readonly StudentManagementDbContext _context;
        private readonly IPasswordHashing _passwordHashing;

        public SettingRepository(StudentManagementDbContext context, IPasswordHashing passwordHashing)
        {
            _context = context;
            _passwordHashing = passwordHashing;
        }

        public User ResetPassword(string formerPassword, string newPassword, Guid userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return null;

            var salt = Convert.FromBase64String(user.PasswordSalt);
            var isPasswordValid = _passwordHashing.VerifyPassword(formerPassword, user.PasswordHash, salt);

            if (!isPasswordValid)
                return null;

            var newSalt = _passwordHashing.GenerateSalt();
            var newHashedPassword = _passwordHashing.HashPassword(newPassword, newSalt);

            user.PasswordHash = newHashedPassword;
            user.PasswordSalt = Convert.ToBase64String(newSalt);

            _context.Users.Update(user);
            _context.SaveChanges();

            return user;
        }
    }

}
