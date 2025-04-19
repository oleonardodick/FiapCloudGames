using FiapCloudGames.API.Messages;
using System.Net;

namespace FiapCloudGames.API.Exceptions
{
    public class InvalidLoginException : FiapCloudGamesException
    {
        public InvalidLoginException() : base (AppMessages.InvalidLoginMessage){ }
        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
