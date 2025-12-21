using FCG.Application.Abstractions.Persistence;
using FCG.Application.Abstractions.Security;
using FCG.Domain.Entities;
using FCG.Domain.Errors;
using FCG.Domain.Exceptions;
using FCG.Domain.Policies;
using System.Text.RegularExpressions;

namespace FCG.Application.UseCases.Auth.RegisterUser
{
    public class RegisterUserHandler
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;

        public RegisterUserHandler(IUserRepository users, IPasswordHasher hasher)
        {
            _users = users;
            _hasher = hasher;
        }

        public async Task<Guid> Handle(RegisterUserCommand cmd)
        {
            var normalizedEmail = cmd.Email.Trim().ToLowerInvariant()
                ;

            var emailResult = EmailPolicy.Validate(normalizedEmail);
            if (!emailResult.IsValid)
                throw new DomainException(emailResult.Error!);

            var passwordResult = PasswordPolicy.Validate(cmd.Password);
            if (!passwordResult.IsValid) throw new DomainException(passwordResult.Error!);

            if (await _users.ExistsByEmail(normalizedEmail))
                throw new DomainException(DomainErrors.User.EmailAlreadyExists);

            var normalizedName = Regex.Replace(cmd.Name.Trim(), @"\s+", " ");

            var hash = _hasher.Hash(cmd.Password);

            var user = new User(normalizedName, normalizedEmail, hash);

            var createdUserId = await _users.Add(user);

            return createdUserId;
        }
    }
}
