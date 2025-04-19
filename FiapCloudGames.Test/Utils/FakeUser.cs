using Bogus;
using FiapCloudGames.API.Entities;

namespace FiapCloudGames.Test.Utils
{
    public static class FakeUser
    {
        public static List<User> FakeListUsers(int qtToGenerate)
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => f.Random.Guid())
                .RuleFor(u => u.CreatedAt, f => f.Date.Recent(30))
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => CreatePassword(8))
                .RuleFor(u => u.RoleId, f => Guid.NewGuid());
            return userFaker.Generate(qtToGenerate);
        }

        public static User FakeSpecificUser()
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => f.Random.Guid())
                .RuleFor(u => u.CreatedAt, f => f.Date.Recent(30))
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => CreatePassword(8))
                .RuleFor(u => u.RoleId, f => Guid.NewGuid());
            return userFaker.Generate();
        }

        private static string CreatePassword(int size)
        {
            var rand = new Random();
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specialCharacters = "!@#$%&*()-_<>?:;^~][{}";

            var password = new[]
            {
                upperCase[rand.Next(upperCase.Length)],
                lowerCase[rand.Next(lowerCase.Length)],
                numbers[rand.Next(numbers.Length)],
                specialCharacters[rand.Next(specialCharacters.Length)]
            }.ToList();

            var allCharacteres = upperCase + lowerCase + numbers + specialCharacters;
            password.AddRange(Enumerable.Range(0, size - password.Count)
                .Select(_ => allCharacteres[rand.Next(allCharacteres.Length)]));

            return new string(password.OrderBy(_ => rand.Next()).ToArray());

        }
    }
}
