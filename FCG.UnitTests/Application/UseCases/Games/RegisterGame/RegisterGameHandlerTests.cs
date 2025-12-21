using FCG.Application.Abstractions.Persistence;
using FCG.Application.UseCases.Games.RegisterGame;
using FCG.Domain.Entities;
using FCG.Domain.Errors;
using FCG.Domain.Exceptions;
using Moq;
using Xunit;

namespace FCG.UnitTests.Application.UseCases.Games.RegisterGame
{
    public class RegisterGameHandlerTests
    {
        private readonly Mock<IGameRepository> _games = new();

        private RegisterGameHandler CreateSut()
            => new(_games.Object);

        [Fact]
        public async Task Deve_lancar_erro_quando_titulo_for_vazio()
        {
            var sut = CreateSut();

            var cmd = new RegisterGameCommand(
                Title: "   ",
                Description: "Descricao",
                Price: 10m
            );

            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));

            Assert.Equal(DomainErrors.Game.TitleIsNullOrWhiteSpace.Code, ex.Code);
        }

        [Fact]
        public async Task Deve_lancar_erro_quando_preco_for_negativo()
        {
            var sut = CreateSut();

            var cmd = new RegisterGameCommand(
                Title: "God of Code",
                Description: "Descricao",
                Price: -5m
            );

            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));

            Assert.Equal(DomainErrors.Game.PriceIsNegative.Code, ex.Code);
        }

        [Fact]
        public async Task Deve_lancar_erro_quando_titulo_ja_existir()
        {
            var sut = CreateSut();

            _games.Setup(g => g.ExistsByTitle("God of Code"))
                  .ReturnsAsync(true);

            var cmd = new RegisterGameCommand(
                Title: " God of Code ",
                Description: "Descricao",
                Price: 50m
            );

            var ex = await Assert.ThrowsAsync<DomainException>(() => sut.Handle(cmd));

            Assert.Equal(DomainErrors.Game.TitleAlreadyExists.Code, ex.Code);
        }

        [Fact]
        public async Task Deve_cadastrar_jogo_quando_dados_forem_validos()
        {
            var sut = CreateSut();

            _games.Setup(g => g.ExistsByTitle("God of Code"))
                  .ReturnsAsync(false);

            var gameId = Guid.NewGuid();

            _games.Setup(g => g.Add(It.IsAny<Game>()))
                  .ReturnsAsync(gameId);

            var cmd = new RegisterGameCommand(
                Title: "God of Code",
                Description: "Um jogo épico.",
                Price: 99.90m
            );

            var result = await sut.Handle(cmd);

            Assert.Equal(gameId, result);
            _games.Verify(g => g.Add(It.IsAny<Game>()), Times.Once);
        }
    }
}
