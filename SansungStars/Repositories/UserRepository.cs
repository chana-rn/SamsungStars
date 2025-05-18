using System.Text.Json;
using WebApplication1;


namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> login(String Email, String Password)
        {
            using (StreamReader reader = System.IO.File.OpenText("Users.txt"))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user != null && user.Email == Email && user.Password == Password)
                    {
                        return Task.FromResult(user);
                    }
                }
            }

            return null;
        }

        public Task<User> Register(String Email, String Password, String FirstName, String LastName)
        {

            Console.WriteLine("repos");
            int numberOfUsers = System.IO.File.ReadLines("Users.txt").Count();
            int id = numberOfUsers + 1;
            User user = new User(Email, Password, FirstName, LastName, id);
            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText("Users.txt", userJson + Environment.NewLine);
            return Task.FromResult(user); 
        }

        public Task<User> update(User userToUpdate)
        {
            string textToReplace = string.Empty;
            
            using (StreamReader reader = System.IO.File.OpenText("Users.txt"))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {

                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.Id == userToUpdate.Id)
                    {
                        textToReplace = currentUserInFile;
                        break;
                    }
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText("Users.txt");
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                System.IO.File.WriteAllText("Users.txt", text);
            }
            return Task.FromResult(userToUpdate); ; 
        }
    }
}
