namespace FCG.Domain.Entities
{
    public enum UserRole
    {
        User = 1,
        Admin = 2
    }
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }

        public User(string name, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = UserRole.User;
        }

        protected User() { }

    }
}
