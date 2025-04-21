using Bogus;
using FiapCloudGames.API.Entities;

namespace FiapCloudGames.Test.Utils
{
    public static class FakeGame
    {
        public static List<Game> FakeListGames (int qtToGenerate)
        {
            var gameFaker = new Faker<Game>()
                .RuleFor(g => g.Name, f => f.Random.Word())
                .RuleFor(g => g.Description, f => f.Lorem.Text())
                .RuleFor(g => g.CreatedAt, f => f.Date.Recent(30))
                .RuleFor(g => g.Price, f => f.Random.Double());
            return gameFaker.Generate(qtToGenerate);
        }

        public static Game FakeSpecificGame()
        {
            var userFaker = new Faker<Game>()
                .RuleFor(g => g.CreatedAt, f => f.Date.Recent(30))
                .RuleFor(g => g.Name, f => f.Name.FullName())
                .RuleFor(g => g.Description, f => f.Internet.Email())
                .RuleFor(g => g.Price, f => f.Random.Double());
            return userFaker.Generate();
        }
    }
}
