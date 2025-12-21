using FCG.Application.Abstractions.Persistence;
using FCG.Domain.Entities;
using FCG.Domain.Errors;
using FCG.Domain.Exceptions;
using FCG.Domain.Policies.Game;

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
            var normalizedTitle = cmd.Title?.Trim();
            var normalizedDescription = cmd.Description?.Trim();

            var validation = GamePolicy.Validate(
                normalizedTitle,
                normalizedDescription,
                cmd.Price
            );

            if (!validation.IsValid)
                throw new DomainException(validation.Error!);

            if (await _games.ExistsByTitle(normalizedTitle!))
                throw new DomainException(DomainErrors.Game.TitleAlreadyExists);

            var game = new Game(
                normalizedTitle!,
                normalizedDescription!,
                cmd.Price
            );

            var createdGameId = await _games.Add(game);

            return createdGameId;
        }
    }
}
