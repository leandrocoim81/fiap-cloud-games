using FCG.Application.Common.Errors;
using FCG.Application.Common.Validation;

namespace FCG.Application.Policies.Auth
{
    public static class PasswordPolicy
    {
        public const int MinLength = 8;

        public static ValidationResult Validate(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return ValidationResult.Fail(ApplicationErrors.Password.PasswordIsNullOrWhiteSpace);

            var p = password.Trim();

            if (p.Length < MinLength)
                return ValidationResult.Fail(ApplicationErrors.Password.PasswordTooShort);

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
                return ValidationResult.Fail(ApplicationErrors.Password.PasswordMissingLetter);
            if (!hasDigit)
                return ValidationResult.Fail(ApplicationErrors.Password.PasswordMissingDigit);
            if (!hasSpecial)
                return ValidationResult.Fail(ApplicationErrors.Password.PasswordMissingSpecialCharacter);

            return ValidationResult.Ok();
        }
    }
}
