using FCG.Domain.Errors;

namespace FCG.Domain.Policies
{
    public static class PasswordPolicy
    {
        public const int MinLength = 8;

        public static ValidationResult Validate(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return ValidationResult.Fail(DomainErrors.User.PasswordIsNullOrWhiteSpace);

            var p = password.Trim();

            if (p.Length < MinLength)
                return ValidationResult.Fail(DomainErrors.User.PasswordTooShort);

            bool hasLetter = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach (var c in p)
            {
                if (char.IsLetter(c)) hasLetter = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else hasSpecial = true; // qualquer coisa que não seja letra/dígito
            }

            if (!hasLetter)
                return ValidationResult.Fail(DomainErrors.User.PasswordMissingLetter);
            if (!hasDigit)
                return ValidationResult.Fail(DomainErrors.User.PasswordMissingDigit);
            if (!hasSpecial)
                return ValidationResult.Fail(DomainErrors.User.PasswordMissingSpecialCharacter);

            return ValidationResult.Ok();
        }
    }

    public readonly record struct ValidationResult(bool IsValid, DomainError? Error)
    {
        public static ValidationResult Ok() => new(true, null);
        public static ValidationResult Fail(DomainError error) => new(false, error);
    }
}
