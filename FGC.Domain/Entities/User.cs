using System;
using System.Collections.Generic;
using System.Text;

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

        public User(string Name, string Email, string PasswordHash)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Email = Email;
            this.PasswordHash = PasswordHash;
            this.Role = Role;
        }

        protected User() { }

    }
}
