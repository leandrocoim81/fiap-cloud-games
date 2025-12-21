using FCG.Application.Abstractions.Persistence;
using FCG.Domain.Entities;
using FCG.Domain.Errors;
using FCG.Domain.Exceptions;

namespace FCG.Application.UseCases.Library.AddToLibrary
{
    public class AddToLibraryHandler
    {
        private readonly ILibraryItemRepository _libraryItemRepository;
        private readonly IGameRepository _gameRepository;

        public AddToLibraryHandler(ILibraryItemRepository libraryItemRepository, IGameRepository gameRepository)
        {
            _libraryItemRepository = libraryItemRepository;
            _gameRepository = gameRepository;
        }

        public async Task<Guid> Handle(AddToLibraryCommand command)
        {
            if(command.UserId == Guid.Empty)
                throw new DomainException(DomainErrors.Library.UserIdIsEmpty);

            if(command.GameId == Guid.Empty)
                throw new DomainException(DomainErrors.Library.GameIdIsEmpty);

            if(! await _gameRepository.ExistsById(command.GameId))
                throw new DomainException(DomainErrors.Library.GameDoesNotExist);

            if (await _libraryItemRepository.ExistsByUserAndGame(command.UserId, command.GameId))
                throw new DomainException(DomainErrors.Library.GameAlreadyInLibrary);

            var libraryItem = new LibraryItem(
                command.UserId, 
                command.GameId);

            var libraryId = await _libraryItemRepository.Add(libraryItem);

            return libraryId;
        }
    }
}
