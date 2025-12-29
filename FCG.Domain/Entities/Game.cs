using FCG.Domain.Errors;
using FCG.Domain.Exceptions;

public class Game
{
    public const int TitleMaxLength = 120;
    public const int DescriptionMaxLength = 2000;

    public Game(string title, string description, decimal price)
    {
        var normalizedTitle = title?.Trim();
        var normalizedDescription = description?.Trim();

        if (string.IsNullOrWhiteSpace(normalizedTitle))
            throw new DomainException(DomainErrors.Game.TitleIsNullOrWhiteSpace);

        if (string.IsNullOrWhiteSpace(normalizedDescription))
            throw new DomainException(DomainErrors.Game.DescriptionIsNullOrWhiteSpace);

        if (normalizedTitle.Length > TitleMaxLength)
            throw new DomainException(DomainErrors.Game.TitleTooLong);

        if (normalizedDescription.Length > DescriptionMaxLength)
            throw new DomainException(DomainErrors.Game.DescriptionTooLong);

        if (price < 0)
            throw new DomainException(DomainErrors.Game.PriceIsNegative);

        Id = Guid.NewGuid();
        Title = normalizedTitle;
        Description = normalizedDescription;
        Price = price;
    }

    protected Game() { }

    public Guid Id { get; private set; }
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; }
}
