using System.Text.Json;

using Repositories;
using Entities;
namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> login(LoginRequest loginRequest)
        {
            if (loginRequest.Email == null || loginRequest.Password == null)
                return null;
            return await _userRepository.login(loginRequest);
        }

        public async Task<User> Register(User user)
        {
            Console.WriteLine(user.Email + "Service");
            if (user.Email == null || user.Password == null)
                return null;
            return await  _userRepository.Register(user);
        }
        public async Task<User> update(int id, User userToUpdate)
        {
            if (userToUpdate.Email == null || userToUpdate.Password == null)
                throw new Exception();
            return await _userRepository.update(id,userToUpdate);

        }

        public int checkPassword(string passward)
        {
            Console.WriteLine($"Password: {passward}");
            if (passward != null)
            {
                var zxcvbnResult = Zxcvbn.Core.EvaluatePassword(passward);
                return zxcvbnResult.Score;
            }
            return -1;
        }

        public async Task<User> findById(int id)
        {
            return await _userRepository.findById(id);
        }
    }
}
