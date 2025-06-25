using StockApp.Domain.Entities;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace StockApp.Domain.Validation
{
    public class AuthenticationException : Exception
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.Unauthorized;
        public AuthenticationException(string message) : base(message) { }
        public AuthenticationException(string message, Exception innerException) 
            : base(message, innerException) { }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
            {
                throw new ArgumentException(message);
            }
        }        
    }
}
