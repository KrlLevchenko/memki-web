using Dodo.Primitives;

namespace Memki.Domain
{
    public class User
    {
        public User(Uuid id, string login, string? passwordHash)
        {
            Id = id;
            Login = login;
            PasswordHash = passwordHash;
        }
        
        public Uuid Id { get; }
        public string Login { get; }
        public string? PasswordHash { get; }
    }
}