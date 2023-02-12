using Microsoft.AspNetCore.Identity;

namespace Application.Models
{
    public class User : Entity
    {
        public User(string name, string password, IPasswordHasher<User> passwordHasher)
        {
            Name = name;
            HashedPassword = HashPassword(password, passwordHasher); 
        }

        private User(){}

        public string Name { get; private set; }

        public string HashedPassword { get; private set; }

        public bool VerifyPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            return passwordHasher.VerifyHashedPassword(this, HashedPassword, password) ==
                   PasswordVerificationResult.Success;
        }

        private string HashPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            return passwordHasher.HashPassword(this, password);
        }
    }
}
