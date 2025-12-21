using FCG.Application.Abstractions.Persistence;
using FCG.Application.UseCases.Library.AddToLibrary;
using FCG.Domain.Entities;
using FCG.Domain.Errors;
using FCG.Domain.Exceptions;
using Moq;
using Xunit;

namespace FCG.UnitTests.Application.UseCases.Library.AddToLibrary
{
    public class AddToLibraryHandlerTests
    {
        private readonly Mock<ILibraryItemRepository> _libraryItems = new();
        private readonly Mock<IGameRepository> _games = new();
        private AddToLibraryHandler CreateSut()
            => new(_libraryItems.Object, _games.Object);

        [Fact]
        public async Task Deve_lancar_erro_quando_usuario_for_vazio()
        {
            var sut = CreateSut();
            var cmd = new AddToLibraryCommand(
                UserId: Guid.Empty,
                GameId: Guid.NewGuid()
            );
            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));
            Assert.Equal(DomainErrors.Library.UserIdIsEmpty.Code, ex.Code);
        }
        [Fact]
        public async Task Deve_lancar_erro_quando_jogo_for_vazio()
        {
            var sut = CreateSut();
            var cmd = new AddToLibraryCommand(
                UserId: Guid.NewGuid(),
                GameId: Guid.Empty
            );
            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));
            Assert.Equal(DomainErrors.Library.GameIdIsEmpty.Code, ex.Code);
        }
        [Fact]
        public async Task Deve_lancar_erro_quando_jogo_nao_existir()
        {
            var sut = CreateSut();
            var gameId = Guid.NewGuid();
            _games.Setup(g => g.ExistsById(gameId))
                  .ReturnsAsync(false);
            var cmd = new AddToLibraryCommand(
                UserId: Guid.NewGuid(),
                GameId: gameId
            );
            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));
            Assert.Equal(DomainErrors.Library.GameDoesNotExist.Code, ex.Code);
        }
        [Fact]
        public async Task Deve_lancar_erro_quando_jogo_ja_estiver_na_biblioteca()
        {
            var sut = CreateSut();
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _games.Setup(g => g.ExistsById(gameId))
                  .ReturnsAsync(true);
            _libraryItems.Setup(l => l.ExistsByUserAndGame(userId, gameId))
                         .ReturnsAsync(true);
            var cmd = new AddToLibraryCommand(
                UserId: userId,
                GameId: gameId
            );
            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));
            Assert.Equal(DomainErrors.Library.GameAlreadyInLibrary.Code, ex.Code);
        }
        [Fact]
        public async Task Deve_adicionar_jogo_quando_dados_forem_validos()
        {
            var sut = CreateSut();
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();
            _games.Setup(g => g.ExistsById(gameId))
                  .ReturnsAsync(true);
            _libraryItems.Setup(l => l.ExistsByUserAndGame(userId, gameId))
                         .ReturnsAsync(false);
            _libraryItems.Setup(l => l.Add(It.IsAny<LibraryItem>()))
                         .ReturnsAsync(Guid.NewGuid());
            var cmd = new AddToLibraryCommand(
                UserId: userId,
                GameId: gameId
            );
            var result = await sut.Handle(cmd);
            Assert.NotEqual(Guid.Empty, result);
        }

    }

}
