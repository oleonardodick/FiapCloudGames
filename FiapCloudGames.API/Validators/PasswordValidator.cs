namespace FiapCloudGames.API.Validators
{
    public static class PasswordValidator
    {
        public static bool HasValidFormat(string password)
        {
            return HaveUpperCase(password) && HaveLowerCase(password) && HaveNumber(password) && HaveSpecialCharactere(password);
        }

        private static bool HaveUpperCase(string password) => password.Any(char.IsUpper);
        private static bool HaveLowerCase(string password) => password.Any(char.IsLower);
        private static bool HaveNumber(string password) => password.Any(char.IsDigit);
        private static bool HaveSpecialCharactere(string password)
        {
            var specialCharacteres = "!@#$%&*()-_<>?:;^~][{}";
            return password.Any(c => specialCharacteres.Contains(c));
        }
    }
}
