using System.Net;

namespace FiapCloudGames.API.Exceptions
{
    public class NotFoundException : FiapCloudGamesException
    {
        public NotFoundException(string message) : base(message) { }
        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
