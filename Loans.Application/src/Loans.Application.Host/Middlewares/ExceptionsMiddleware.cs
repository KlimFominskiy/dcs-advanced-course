using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Loans.Application.AppServices.Contracts.Clients.Exceptions;
using Loans.Application.AppServices.Contracts.Loans.Exceptions;
using Loans.Application.AppServices.Models;

namespace Loans.Application.Host.Middlewares;

/// <summary>
/// Middleware для обработки пользовательских исключений валидации и возврата соответствующих JSON-ответов.
/// </summary>
public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Инициализирует новый экземпляр класса CustomValidationExceptionsMiddleware>.
    /// </summary>
    /// <param name="next">Следующий делегат запроса в конвейере.</param>
    public ExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Вызывает middleware для обработки исключений и генерации JSON-ответов.
    /// </summary>
    /// <param name="httpContext">HTTP-контекст текущего запроса.</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }
    
    /// <summary>
    /// Обрабатывает исключение и генерирует JSON-ответ на основе типа исключения.
    /// </summary>
    /// <param name="httpContext">HTTP-контекст текущего запроса.</param>
    /// <param name="exception">Исключение, которое было перехвачено.</param>
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        ErrorDetails errorDetails;
        if (exception is ClientValidationException clientValidationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            errorDetails = new ErrorDetails()
            {
                Error = clientValidationException.Message,
                Details = clientValidationException.ValidationErrors
            };
        }
        else if (exception is LoanValidationException loanValidationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            errorDetails = new ErrorDetails()
            {
                Error = loanValidationException.Message,
                Details = loanValidationException.ValidationErrors
            };
        }
        else
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            errorDetails = new ErrorDetails()
            {
                Error = exception.Message,
                Details = new()
                {
                    exception.Source ?? string.Empty,
                    exception.HelpLink ?? string.Empty,
                    exception.StackTrace ?? string.Empty,
                }
            };
        }
        string jsonResponse = JsonSerializer.Serialize(errorDetails);
        await httpContext.Response.WriteAsync(jsonResponse);
    }
}