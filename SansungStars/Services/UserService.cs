using System.Text.Json;

using Repositories;
using Entities;
using Microsoft.Extensions.Logging;
namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> login(LoginRequest loginRequest)
        {
            if (loginRequest.Email == null || loginRequest.Password == null)
                return null;

            var user = await _userRepository.login(loginRequest);
            if (user != null)
                _logger.LogInformation("User {Email} logged in successfully", user.Email);

            return user;

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
            if (passward != null && passward!="")
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
