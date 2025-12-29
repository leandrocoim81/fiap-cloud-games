using FCG.SharedKernel;

namespace FCG.Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly ErrorCode EmailAlreadyExists =
                new("USER_EMAIL_ALREADY_EXISTS", "E-mail já cadastrado."); 

            public static readonly ErrorCode PasswordHashIsNullOrWhiteSpace =
                new("USER_PASSWORD_HASH_NULL_OR_WHITESPACE", "Hash da senha é obrigatório.");

            public static readonly ErrorCode EmailIsNullOrWhiteSpace =
                new("USER_EMAIL_NULL_OR_WHITESPACE", "E-mail é obrigatório.");

            public static readonly ErrorCode NameIsNullOrWhiteSpace =
                new("USER_NAME_NULL_OR_WHITESPACE", "Nome é obrigatório.");
            
            public static readonly ErrorCode InvalidEmail =
                new("USER_EMAIL_INVALID", "E-mail inválido.");
        }

        public static class Auth
        {
            public static readonly ErrorCode InvalidCredentials =
                new("AUTH_INVALID_CREDENTIALS", "Credenciais inválidas.");
        }

        public static class Game
        {
            public static readonly ErrorCode TitleIsNullOrWhiteSpace =
                new("GAME_TITLE_NULL_OR_WHITESPACE", "O título do jogo é obrigatório.");

            public static readonly ErrorCode DescriptionIsNullOrWhiteSpace =
                new("GAME_DESCRIPTION_NULL_OR_WHITESPACE", "A descrição do jogo é obrigatória.");

            public static readonly ErrorCode PriceIsNegative =
                new("GAME_PRICE_NEGATIVE", "O preço do jogo não pode ser negativo.");

            public static readonly ErrorCode DescriptionTooLong =
                new("GAME_DESCRIPTION_TOO_LONG", "A descrição do jogo é muito longa.");

            public static readonly ErrorCode TitleTooLong =
                new("GAME_TITLE_TOO_LONG", "O título do jogo é muito longo.");

            public static readonly ErrorCode TitleAlreadyExists =
                new("GAME_TITLE_ALREADY_EXISTS", "Já existe um jogo cadastrado com este título.");            

        }

        public static class Library
        {
            public static readonly ErrorCode GameIdIsEmpty =
                new("LIBRARY_GAME_ID_IS_EMPTY", "GameId é obrigatório");            

            public static readonly ErrorCode UserIdIsEmpty = 
                new("LIBRARY_USER_ID_IS_EMPTY", "UserId é obrigatório");

            public static readonly ErrorCode GameAlreadyInLibrary =
                new("LIBRARY_GAME_ALREADY_IN_LIBRARY", "O jogo já está na biblioteca do usuário.");

            public static ErrorCode GameDoesNotExist =
                new("LIBRARY_GAME_DOES_NOT_EXIST", "O jogo não existe.");
        }
    }
}

