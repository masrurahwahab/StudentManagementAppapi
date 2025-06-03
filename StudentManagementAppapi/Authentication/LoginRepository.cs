using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Authentication
{
    public class LoginRepository : ILoginRepository
    {
        private readonly StudentManagementDbContext _context;

        public LoginRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);            
        }

        public bool IsExxist(Func<User, bool> expression)
        {
            return _context.Users.Any(expression);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }

}
