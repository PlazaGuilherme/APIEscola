using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace APIEscola.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu durante o processamento da requisição: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var response = new ErrorResponse();
            
            switch (exception)
            {
                case ArgumentNullException argEx:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = $"Parâmetro obrigatório não informado: {argEx.ParamName}";
                    response.Details = argEx.Message;
                    break;

                case ArgumentException argEx:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Dados inválidos";
                    response.Details = argEx.Message;
                    break;

                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Recurso não encontrado";
                    response.Details = exception.Message;
                    break;

                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "Acesso não autorizado";
                    response.Details = exception.Message;
                    break;

                case InvalidOperationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Operação inválida";
                    response.Details = exception.Message;
                    break;

                case DbUpdateException dbEx:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Erro ao processar dados no banco";
                    
                    if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("UNIQUE"))
                    {
                        response.Details = "Violação de integridade: registro duplicado";
                    }
                    else if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("FK"))
                    {
                        response.Details = "Violação de integridade: referência inválida";
                    }
                    else
                    {
                        response.Details = dbEx.InnerException?.Message ?? dbEx.Message;
                    }
                    break;


                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Erro interno do servidor";
                    response.Details = exception.Message;
                    break;
            }

            context.Response.StatusCode = response.StatusCode;
            
            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? Path { get; set; }
    }
}

