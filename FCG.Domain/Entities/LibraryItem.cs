namespace FCG.Domain.Entities
{
    public class LibraryItem
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid GameId { get; private set; }
        public DateTime AddedAt { get; private set; }

        protected LibraryItem() { }
        public LibraryItem(Guid userId, Guid gameId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            GameId = gameId;
            AddedAt = DateTime.UtcNow;
        }
    }
}

