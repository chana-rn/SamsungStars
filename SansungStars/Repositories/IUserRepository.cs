using WebApplication1;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> login(string Email, string Password);
        Task<User> Register(string Email, string Password, string FirstName, string LastName);
        Task<User> update(User userToUpdate);
    }
}