using FiapCloudGames.API.Messages;
using System.Net;

namespace FiapCloudGames.API.Exceptions
{
    public class InvalidTokenException : FiapCloudGamesException
    {
        public InvalidTokenException() : base(AppMessages.InvalidTokenMessage) { }
        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
