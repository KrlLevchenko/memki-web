using Dodo.Primitives;

namespace Memki.Model.Auth.Entities
{
    public class User
    {
        public User(Uuid id, string name, string email, string? passwordHash)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
        }
        
        public Uuid Id { get; }
        public string Email { get; }
        public string Name { get; }
        public string? PasswordHash { get; set; }

        public void SetPasswordHash(string? passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}