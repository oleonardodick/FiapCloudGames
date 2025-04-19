using System.Net;

namespace FiapCloudGames.API.Exceptions
{
    public class EmailAlreadyExistsException : FiapCloudGamesException
    {
        public EmailAlreadyExistsException(string message) : base(message) { }
        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
