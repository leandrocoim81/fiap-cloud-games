
using FCG.Application.Abstractions.Persistence;
using FCG.Application.Abstractions.Security;
using FCG.Application.Common.Exceptions;
using FCG.Application.Policies.Auth;
using FCG.Domain.Entities;
using FCG.Domain.Errors;
using FCG.Domain.ValueObjects;

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
            var email = new Email(cmd.Email);

            var passwordResult = PasswordPolicy.Validate(cmd.Password);
            if (!passwordResult.IsValid) throw new ValidationException(passwordResult.Error!);

            if (await _users.ExistsByEmail(email.Value))
                throw new ConflictException(DomainErrors.User.EmailAlreadyExists);

            var hash = _hasher.Hash(cmd.Password);

            var user = new User(cmd.Name, email, hash);

            var createdUserId = await _users.Add(user);

            return createdUserId;
        }
    }
}
