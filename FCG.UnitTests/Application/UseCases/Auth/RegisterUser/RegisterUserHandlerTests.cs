using FCG.Application.Abstractions.Persistence;
using FCG.Application.Abstractions.Security;
using FCG.Application.UseCases.Auth.RegisterUser;
using FCG.Domain.Entities;
using FCG.Domain.Errors;
using FCG.Domain.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FCG.UnitTests.Application.UseCases.Auth.RegisterUser
{
    public class RegisterUserHandlerTests
    {
        private readonly Mock<IUserRepository> _users = new();
        private readonly Mock<IPasswordHasher> _hasher = new();

        private RegisterUserHandler CreateSut()
            => new(_users.Object, _hasher.Object);

        [Fact]
        public async Task Deve_lancar_erro_quando_email_for_invalido()
        {
            var sut = CreateSut();

            var cmd = new RegisterUserCommand(
                Name: "Leandro",
                Email: "email-invalido",
                Password: "Abcdef1!"
            );

            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));

            Assert.Equal(DomainErrors.User.InvalidEmail.Code, ex.Code);

            // garante que não foi nem tentar no repositório/hasher
            _users.Verify(r => r.ExistsByEmail(It.IsAny<string>()), Times.Never);
            _hasher.Verify(h => h.Hash(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Deve_lancar_erro_quando_senha_for_invalida()
        {
            var sut = CreateSut();

            var cmd = new RegisterUserCommand(
                Name: "Leandro",
                Email: "leandro@email.com",
                Password: "123" // inválida
            );

            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));

            // sua PasswordPolicy pode retornar erros diferentes (TooShort, MissingLetter etc.)
            // então aqui a gente só garante que é um erro de password do catálogo:
            Assert.StartsWith("USER_PASSWORD_", ex.Code);

            _users.Verify(r => r.ExistsByEmail(It.IsAny<string>()), Times.Never);
            _hasher.Verify(h => h.Hash(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Deve_lancar_erro_quando_email_ja_existir()
        {
            var sut = CreateSut();

            _users.Setup(r => r.ExistsByEmail("teste@email.com"))
                  .ReturnsAsync(true);

            var cmd = new RegisterUserCommand(
                Name: "Leandro",
                Email: "  TESTE@Email.com  ", // deve normalizar
                Password: "Abcdef1!"
            );

            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));

            Assert.Equal(DomainErrors.User.EmailAlreadyExists.Code, ex.Code);

            _users.Verify(r => r.ExistsByEmail("teste@email.com"), Times.Once);
            _hasher.Verify(h => h.Hash(It.IsAny<string>()), Times.Never);
            _users.Verify(r => r.Add(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Deve_criar_usuario_e_retornar_id_do_repositorio()
        {
            var sut = CreateSut();

            _users.Setup(r => r.ExistsByEmail("leandro@email.com"))
                  .ReturnsAsync(false);

            _hasher.Setup(h => h.Hash("Abcdef1!"))
                   .Returns("HASH");

            var repoId = Guid.NewGuid();
            User? savedUser = null;

            _users.Setup(r => r.Add(It.IsAny<User>()))
                  .Callback<User>(u => savedUser = u)
                  .ReturnsAsync(repoId);

            var cmd = new RegisterUserCommand(
                Name: "Leandro",
                Email: "  Leandro@Email.com  ",
                Password: "Abcdef1!"
            );

            var id = await sut.Handle(cmd);

            Assert.Equal(repoId, id);

            _users.Verify(r => r.ExistsByEmail("leandro@email.com"), Times.Once);
            _hasher.Verify(h => h.Hash("Abcdef1!"), Times.Once);
            _users.Verify(r => r.Add(It.IsAny<User>()), Times.Once);

            Assert.NotNull(savedUser);
            Assert.Equal("leandro@email.com", savedUser!.Email);
            Assert.Equal("HASH", savedUser.PasswordHash);
        }
    }
}
