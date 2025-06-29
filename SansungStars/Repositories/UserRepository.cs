using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;



namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        SamsungStarsContext _samsungStarsContext;
        public UserRepository(SamsungStarsContext samsungStarsContext)
        {
            _samsungStarsContext = samsungStarsContext;
        }
        public async Task<User> login(LoginRequest loginRequest)
        {
            return await _samsungStarsContext.Users.FirstOrDefaultAsync(user => user.Email == loginRequest.Email && user.Password == loginRequest.Password);
        }

        public async Task<User> Register(User user)
        {

            await _samsungStarsContext.Users.AddAsync(user);
            await _samsungStarsContext.SaveChangesAsync();
            //return await Task.FromResult(user);
            return user;
        }

        public async  Task<User> update(int id, User userToUpdate)
        {
            _samsungStarsContext.Users.Update(userToUpdate);
            await _samsungStarsContext.SaveChangesAsync();
            return userToUpdate;
        }

        public async Task<User> findById(int id)
        {
           return await _samsungStarsContext.Users.FindAsync(id);
        }
    }
}
