using FCG.Application.Policies.Auth;
using FCG.SharedKernel;

namespace FCG.Application.Common.Errors
{
    public static class ApplicationErrors
    {
        public static class Password
        {
            public static readonly ErrorCode InvalidPassword =
                new("USER_INVALID_PASSWORD", "Senha não atende aos critérios de segurança.");

            public static readonly ErrorCode PasswordIsNullOrWhiteSpace =
                new("USER_PASSWORD_NULL_OR_WHITESPACE", "Senha é obrigatória.");

            public static readonly ErrorCode PasswordTooShort =
                new("USER_PASSWORD_MIN_LENGTH", $"Senha deve ter no mínimo {PasswordPolicy.MinLength} caracteres.");

            public static readonly ErrorCode PasswordMissingLetter =
                new("USER_PASSWORD_MISSING_LETTER", "Senha deve conter pelo menos uma letra.");

            public static readonly ErrorCode PasswordMissingDigit =
                new("USER_PASSWORD_MISSING_DIGIT", "Senha deve conter pelo menos um número.");

            public static readonly ErrorCode PasswordMissingSpecialCharacter =
                new("USER_PASSWORD_MISSING_SPECIAL_CHARACTER", "Senha deve conter pelo menos um caractere especial.");
        }
    }
}
