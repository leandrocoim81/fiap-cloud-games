using FCG.Domain.Errors;

namespace FCG.Domain.Policies
{
    public static class PasswordPolicy
    {
        public const int MinLength = 8;

        public static PasswordValidationResult Validate(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return PasswordValidationResult.Fail(DomainErrors.User.PasswordIsNullOrWhiteSpace);

            var p = password.Trim();

            if (p.Length < MinLength)
                return PasswordValidationResult.Fail(DomainErrors.User.PasswordTooShort);

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
                return PasswordValidationResult.Fail(DomainErrors.User.PasswordMissingLetter);
            if (!hasDigit)
                return PasswordValidationResult.Fail(DomainErrors.User.PasswordMissingDigit);
            if (!hasSpecial)
                return PasswordValidationResult.Fail(DomainErrors.User.PasswordMissingSpecialCharacter);

            return PasswordValidationResult.Ok();
        }
    }

    public readonly record struct PasswordValidationResult(bool IsValid, DomainError? Error)
    {
        public static PasswordValidationResult Ok() => new(true, null);
        public static PasswordValidationResult Fail(DomainError error) => new(false, error);
    }
}
