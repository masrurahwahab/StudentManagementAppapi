using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;
using StudentManagementAppapi.PasswordValidation;

namespace StudentManagementAppapi.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IPasswordHashing _passwordHashing;

        public AuthService(ILoginRepository loginRepository, IPasswordHashing passwordHashing)
        {
            _loginRepository = loginRepository;
            _passwordHashing = passwordHashing;
        }

        public bool Register(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return false;

            var existingUser = _loginRepository.GetUserByEmail(email);
            if (existingUser != null)
                return false;

            var salt = _passwordHashing.GenerateSalt();
            var hash = _passwordHashing.HashPassword(password, salt);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHash = hash,
                PasswordSalt = Convert.ToBase64String(salt),
                Role = Role.Student,
                ConfirmPassword= confirmPassword
            };

            _loginRepository.CreateUser(newUser);
            _loginRepository.SaveChanges(); 
            return true;
        }

        public bool Login(string email, string password)
        {
            var user = _loginRepository.GetUserByEmail(email);
            if (user == null)
                return false;

            var salt = Convert.FromBase64String(user.PasswordSalt); 
            return _passwordHashing.VerifyPassword(password, user.PasswordHash, salt);
        }
    }

}

