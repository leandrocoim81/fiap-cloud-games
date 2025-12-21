namespace FCG.Application.UseCases.Library.AddToLibrary
{
    public record AddToLibraryCommand(Guid UserId, Guid GameId);
}
