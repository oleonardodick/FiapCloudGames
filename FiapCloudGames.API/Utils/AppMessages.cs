namespace FiapCloudGames.API.Utils
{
    public static class AppMessages
    {
        //External messages
        #region User validation
        public const string InvalidEmailFormatMessage = "O e-mail informado não está em um formato válido";
        public const string InvalidPasswordFormatMessage = "A senha deve possuir letras maiúsculas, minúsculas, números e um dos seguintes caracteres !@#$%&*()-_<>?:;^~][{}";
        public const string InvalidSizePasswordMessage = "A senha deve possuir entre 8 e 30 caracteres";
        public const string EmailAlreadyExistsMessage = "O e-mail informado já está cadastrado";
        #endregion

        #region Not Found messages
        public const string UserNotFoundMessage = "Usuário não encontrado";
        public const string RoleNotFoundMessage = "Role não encontrada";
        public const string GameNotFoundMessage = "Jogo não encontrado";
        #endregion

        #region Login messages
        public const string InvalidLoginMessage = "E-mail ou senha inválidos";
        public const string InvalidTokenMessage = "O token JWT não foi enviado, é inválido ou está expirado";
        public const string ForbiddenMessage = "O usuário não possui direitos suficientes para realizar esta operação";
        #endregion

        //Internal messages
        public const string JwtSectionNotConfigured = "A configuração 'JwtSettings' não foi encontrada no appsettings.json";
        public const string SecretKeyNotConfigured = "A chave secreta do JWT não pode ser nula ou vazia";

        //Methods
        public static string GetRequiredFieldMessage(string fieldName)
        {
            var RequiredFieldMessage = "O campo '{0}' é obrigatório";
            return string.Format(RequiredFieldMessage, fieldName);
        }
    }
}
