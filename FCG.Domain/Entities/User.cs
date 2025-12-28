using FCG.Domain.Errors;
using FCG.Domain.Exceptions;
using FCG.Domain.ValueObjects;

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
        public Email Email { get; private set; } = null!;
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }

        public User(string name, Email email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(DomainErrors.User.NameIsNullOrWhiteSpace);

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException(DomainErrors.User.EmailIsNullOrWhiteSpace);

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException(DomainErrors.User.PasswordHashIsNullOrWhiteSpace);

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = UserRole.User;
        }

        protected User() { }

    }
}
