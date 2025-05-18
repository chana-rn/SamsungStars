using System.Text.Json;
using WebApplication1;
using Repositories;
namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> login(String Email, String Password)
        {
            if (Email == null || Password == null)
                return null;
            return await _userRepository.login(Email, Password);
        }

        public async Task<User> Register(String Email, String Password, String FirstName, String LastName)
        {
            Console.WriteLine(Email + "Service");
            if (Email == null || Password == null)
                return null;
            return await  _userRepository.Register(Email, Password,FirstName,LastName);
        }
        public async Task<User> update(User userToUpdate)
        {
            if (userToUpdate.Email == null || userToUpdate.Password == null)
                throw new Exception();
            return await _userRepository.update(userToUpdate);

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
    }
}
