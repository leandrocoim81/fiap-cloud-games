using FCG.Domain.Errors;
using FCG.Domain.Validation;

namespace FCG.Domain.Policies.Game
{
    public static class GamePolicy
    {
        public const int TitleMaxLength = 120;
        public const int DescriptionMaxLength = 2000;

        public static ValidationResult Validate(
            string? title,
            string? description,
            decimal price)
        { 
            if (string.IsNullOrWhiteSpace(title))
                return ValidationResult.Fail(DomainErrors.Game.TitleIsNullOrWhiteSpace);
            
            if (title.Trim().Length > TitleMaxLength)
                return ValidationResult.Fail(DomainErrors.Game.TitleTooLong);

            if(price < 0)
                return ValidationResult.Fail(DomainErrors.Game.PriceIsNegative);

            if (!string.IsNullOrWhiteSpace(description) &&
            description.Trim().Length > DescriptionMaxLength)
                return ValidationResult.Fail(DomainErrors.Game.DescriptionTooLong);

            return ValidationResult.Ok();
        }
    }
}
