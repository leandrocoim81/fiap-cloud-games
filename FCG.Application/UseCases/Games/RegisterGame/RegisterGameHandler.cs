using FCG.Application.Abstractions.Persistence;
using FCG.Domain.Errors;
using FCG.Domain.Exceptions;

namespace FCG.Application.UseCases.Games.RegisterGame
{
    public class RegisterGameHandler
    {
        private readonly IGameRepository _games;

        public RegisterGameHandler(IGameRepository games)
        {
            _games = games;
        }

        public async Task<Guid> Handle(RegisterGameCommand cmd)
        {    

            var game = new Game(
                cmd.Title,
                cmd.Description,
                cmd.Price
            );

            if (await _games.ExistsByTitle(game.Title))
                throw new DomainException(DomainErrors.Game.TitleAlreadyExists);

            var createdGameId = await _games.Add(game);

            return createdGameId;
        }
    }
}
