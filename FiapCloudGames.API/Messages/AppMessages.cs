namespace FiapCloudGames.API.Messages
{
    public static class AppMessages
    {
        //External messages
        public const string InvalidLoginMessage = "E-mail ou senha inválidos";
        public const string UserNotFoundMessage = "Usuário não encontrado";
        public const string InvalidEmailFormatMessage = "O e-mail informado não está em um formato válido";
        public const string InvalidPasswordFormatMessage = "A senha deve possuir no mínimo 8 caracteres, com letras maiúsculas, minúsculas, números e um dos seguintes caracteres !@#$%&*()-_<>?:;^~][{}";
        public const string RequiredFieldMessage = "O campo '{0}' é obrigatório";
        public const string EmailAlreadyExistsMessage = "O e-mail informado já está cadastrado";

        //Internal messages
        public const string JwtSectionNotConfigured = "A configuração 'JwtSettings' não foi encontrada no appsettings.json";
        public const string SecretKeyNotConfigured = "A chave secreta do JWT não pode ser nula ou vazia";

        //Methods
        public static string GetRequiredFieldMessage(string fieldName)
        {
            return string.Format(RequiredFieldMessage, fieldName);
        }
    }
}
