using FCG.Domain.Errors;
using FCG.Domain.Exceptions;
using FCG.Domain.ValueObjects;
using System.Text.RegularExpressions;

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
            var normalizedName = Regex.Replace(name.Trim(), @"\s+", " ");
            var normalizedHash = (passwordHash ?? "").Trim();

            if (string.IsNullOrWhiteSpace(normalizedName))
                throw new DomainException(DomainErrors.User.NameIsNullOrWhiteSpace);

            if (email is null)
                throw new DomainException(DomainErrors.User.EmailIsNullOrWhiteSpace);

            if (string.IsNullOrWhiteSpace(normalizedHash))
                throw new DomainException(DomainErrors.User.PasswordHashIsNullOrWhiteSpace);

            Id = Guid.NewGuid();
            Name = normalizedName;
            Email = email;
            PasswordHash = normalizedHash;
            Role = UserRole.User;
        }

        protected User() { }

    }
}
