namespace FCG.Application.Abstractions.Persistence
{
    public interface IGameRepository
    {
        Task<bool> ExistsByTitle(string title);

        Task<bool> ExistsById(Guid Id);
        Task<Guid> Add(Game game);
    }
}
