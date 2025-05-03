using FiapCloudGames.API.Shared.Utils;
using System.Net;

namespace FiapCloudGames.API.Exceptions
{
    public class ForbiddenException : FiapCloudGamesException
    {
        public ForbiddenException() : base(AppMessages.ForbiddenMessage)
        {
            
        }
        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
