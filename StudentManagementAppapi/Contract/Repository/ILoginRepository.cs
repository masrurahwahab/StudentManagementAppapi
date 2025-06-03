using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface ILoginRepository
    {
        User GetUserByEmail(string email);
        User GetUserByUsername(string username);
        void CreateUser(User user);
        bool IsExxist(Func<User, bool> expression);
        int SaveChanges();
    }

}
