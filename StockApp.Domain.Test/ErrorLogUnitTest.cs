using FluentAssertions;
using StockApp.Domain.Entities;
using StockApp.Domain.Validation;

namespace StockApp.Domain.Test
{
    public class ErrorLogUnitTest
    {
        #region Testes Positivos

        [Fact]
        public void CreateErrorLog_WithValidData_ShouldSucceed()
        {
            var errorLog = new ErrorLog("Exception occurred", "stack trace", "/api/test", "GET", "Postman");

            errorLog.Message.Should().Be("Exception occurred");
            errorLog.Path.Should().Be("/api/test");
            errorLog.Method.Should().Be("GET");
            errorLog.Timestamp.Should().BeBefore(DateTime.UtcNow.AddSeconds(1));
        }

        #endregion

        #region Testes Negativos

        [Fact]
        public void CreateErrorLog_WithEmptyMessage_ShouldThrowDomainException()
        {
            Action act = () => new ErrorLog("", "trace", "/path", "GET", "agent");
            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Message is required.");
        }

        [Fact]
        public void CreateErrorLog_WithEmptyPath_ShouldThrowDomainException()
        {
            Action act = () => new ErrorLog("msg", "trace", "", "POST", "agent");
            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Path is required.");
        }

        [Fact]
        public void CreateErrorLog_WithEmptyMethod_ShouldThrowDomainException()
        {
            Action act = () => new ErrorLog("msg", "trace", "/path", "", "agent");
            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Method is required.");
        }

        [Fact]
        public void CreateErrorLog_WithTooLongMessage_ShouldThrowDomainException()
        {
            var longMessage = new string('a', 1001);
            Action act = () => new ErrorLog(longMessage, "trace", "/path", "POST", "agent");
            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Message is too long.");
        }

        #endregion
    }
}
