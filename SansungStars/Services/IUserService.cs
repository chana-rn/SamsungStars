using WebApplication1;

namespace Services
{
    public interface IUserService
    {
        int checkPassword(string passward);
        Task<User> login(string Email, string Password);
        Task<User> Register(string Email, string Password, string FirstName, string LastName);
        Task<User> update(User userToUpdate);
    }
}