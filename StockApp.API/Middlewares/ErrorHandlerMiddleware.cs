using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Text.Json;

namespace StockApp.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        #region Atributos

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        #endregion

        #region Construtor

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        #region Métodos

        public async Task Invoke(HttpContext context, IErrorLogRepository logRepository)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var errorLog = new ErrorLog(ex.Message, ex.StackTrace, context.Request.Path, context.Request.Method, context.Request.Headers["User-Agent"]);
                await logRepository.SaveAsync(errorLog);

                var result = JsonSerializer.Serialize(new { error = "Erro interno do servidor." });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(result);
            }
        }

        #endregion
    }
}
