using Entities;

namespace Services
{
    public interface IUserService
    {
        int checkPassword(string passward);
        Task<User> login(LoginRequest loginRequest);
        Task<User> Register(User user);
        Task<User> update(int id,User userToUpdate);
        Task<User> findById(int id);

    }
}