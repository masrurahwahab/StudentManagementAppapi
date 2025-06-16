using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;
using StudentManagementAppapi.PasswordValidation;

namespace StudentManagementAppapi.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IPasswordHashing _passwordHashing;
        private readonly IUserRepository _userRepository;

        public AuthService(ILoginRepository loginRepository, IPasswordHashing passwordHashing
             ,IUserRepository userRepository)
        {
            _loginRepository = loginRepository;
            _passwordHashing = passwordHashing;
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return new LoginResponse { Successs = false, Message = "User not found" };

            bool isValid = _passwordHashing.VerifyPassword(password, user.PasswordHash, user.Salt);
            if (!isValid)
                return new LoginResponse { Successs = false, Message = "Invalid password" };

           
            return new LoginResponse
            {
                Successs = true,
                Message = "Login successful",
                
            };
        }


    }

}

