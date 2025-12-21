using FCG.Domain.Policies.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly DomainError EmailAlreadyExists =
                new("USER_EMAIL_ALREADY_EXISTS", "E-mail já cadastrado.");

            public static readonly DomainError InvalidPassword =
                new("USER_INVALID_PASSWORD", "Senha não atende aos critérios de segurança.");

            public static readonly DomainError PasswordIsNullOrWhiteSpace =
                new("USER_PASSWORD_NULL_OR_WHITESPACE", "Senha é obrigatória.");

            public static readonly DomainError PasswordTooShort =
                new("USER_PASSWORD_MIN_LENGTH", $"Senha deve ter no mínimo {PasswordPolicy.MinLength} caracteres.");

            public static readonly DomainError PasswordMissingLetter =
                new("USER_PASSWORD_MISSING_LETTER", "Senha deve conter pelo menos uma letra.");

            public static readonly DomainError PasswordMissingDigit =
                new("USER_PASSWORD_MISSING_DIGIT", "Senha deve conter pelo menos um número.");

            public static readonly DomainError PasswordMissingSpecialCharacter =
                new("USER_PASSWORD_MISSING_SPECIAL_CHARACTER", "Senha deve conter pelo menos um caractere especial.");

            public static readonly DomainError EmailIsNullOrWhiteSpace =
                new("USER_EMAIL_NULL_OR_WHITESPACE", "E-mail é obrigatório.");

            public static readonly DomainError InvalidEmail =
                new("USER_EMAIL_INVALID", "E-mail inválido.");
        }

        public static class Auth
        {
            public static readonly DomainError InvalidCredentials =
                new("AUTH_INVALID_CREDENTIALS", "Credenciais inválidas.");
        }

        public static class Game
        {
            public static readonly DomainError TitleIsNullOrWhiteSpace =
                new("GAME_TITLE_NULL_OR_WHITESPACE", "O título do jogo é obrigatório.");

            public static readonly DomainError TitleAlreadyExists =
                new("GAME_TITLE_ALREADY_EXISTS", "Já existe um jogo cadastrado com este título.");

            public static readonly DomainError PriceIsNegative =
                new("GAME_PRICE_NEGATIVE", "O preço do jogo não pode ser negativo.");

            public static readonly DomainError DescriptionTooLong =
                new("GAME_DESCRIPTION_TOO_LONG", "A descrição do jogo é muito longa.");

            public static readonly DomainError TitleTooLong =
                new("GAME_TITLE_TOO_LONG", "O título do jogo é muito longo.");

        }

        public static class Library
        {
            public static readonly DomainError GameIdIsEmpty =
                new("LIBRARY_GAME_ID_IS_EMPTY", "GameId é obrigatório");            

            public static readonly DomainError UserIdIsEmpty = 
                new("LIBRARY_USER_ID_IS_EMPTY", "UserId é obrigatório");

            public static readonly DomainError GameAlreadyInLibrary =
                new("LIBRARY_GAME_ALREADY_IN_LIBRARY", "O jogo já está na biblioteca do usuário.");

            public static DomainError GameDoesNotExist =
                new("LIBRARY_GAME_DOES_NOT_EXIST", "O jogo não existe.");
        }
    }
}

