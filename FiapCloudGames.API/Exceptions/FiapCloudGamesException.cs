using System.Net;

namespace FiapCloudGames.API.Exceptions
{
    public abstract class FiapCloudGamesException : SystemException
    {
        protected FiapCloudGamesException(string message) : base(message) { }

        public abstract List<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
