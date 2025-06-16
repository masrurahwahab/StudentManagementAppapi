using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        //Task<bool> ValidatePasswordAsync(User user, string password);
       // Task<User> CreateWithPasswordAsync(CreateUserDto createUserDto);
    }
}
