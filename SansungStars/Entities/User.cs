namespace WebApplication1
{
    public class User
    {
       

       
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; } 
       
        public User(string email, string password, string firstName, string lastName, int id)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
