using FCG.Domain.Errors;

namespace FCG.Domain.Validation
{
    public readonly record struct ValidationResult(bool IsValid, DomainError? Error)
    {
        public static ValidationResult Ok() => new(true, null);
        public static ValidationResult Fail(DomainError error) => new(false, error);
    }
}
