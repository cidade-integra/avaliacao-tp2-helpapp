using StockApp.Domain.Validation;

namespace StockApp.Domain.Entities
{
    public class ErrorLog
    {
        #region Atributos

        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string UserAgent { get; set; }

        #endregion

        #region Construtor

        protected ErrorLog() { }

        public ErrorLog(string message, string stackTrace, string path, string method, string userAgent)
        {
            Validate(message, stackTrace, path, method, userAgent);
        }

        #endregion

        #region Validação

        void Validate(string message, string stackTrace, string path, string method, string userAgent)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(message), "Message is required.");
            DomainExceptionValidation.When(message.Length > 1000, "Message is too long.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(path), "Path is required.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(method), "Method is required.");

            Message = message;
            StackTrace = stackTrace;
            Path = path;
            Method = method;
            UserAgent = userAgent;
            Timestamp = DateTime.UtcNow;
        }

        #endregion
    }
}
