using FCG.Domain.Entities;

namespace FCG.Application.Abstractions.Persistence
{
    public interface ILibraryItemRepository
    {
        Task<bool> ExistsByUserAndGame(Guid userId, Guid gameId);
        Task<Guid> Add(LibraryItem libraryItem);
    }
}
