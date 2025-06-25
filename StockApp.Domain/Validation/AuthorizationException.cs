using System.Net;

namespace StockApp.Domain.Validation
{
    public class AuthorizationException : Exception
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.Forbidden;
        public AuthorizationException(string message) : base(message) { }
        public AuthorizationException(string message, Exception innerException)
            : base(message, innerException) { }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
            {
                throw new AuthorizationException(message);
            }
        }
    }
}
