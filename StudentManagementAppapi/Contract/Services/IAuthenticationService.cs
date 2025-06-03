namespace StudentManagementAppapi.Contract.Services
{
    public interface IAuthService
    {
        bool Register(string username, string email, string password,string confirmpassword);
        bool Login(string email, string password);
    }

}
