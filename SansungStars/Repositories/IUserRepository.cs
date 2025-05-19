using Entities;


namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> login(LoginRequest loginRequest);
        Task<User> Register(User user);
        Task<User> update(int id, User userToUpdate);
        Task<User> findById(int id);

    }
}