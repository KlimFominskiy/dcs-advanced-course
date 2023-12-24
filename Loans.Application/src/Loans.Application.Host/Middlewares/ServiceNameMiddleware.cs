namespace Loans.Application.Host.Middlewares;

/// <summary>
/// Middleware, который обогащает Response запроса заголовком с ключом X-SERVICE-NAME
/// </summary>
public class ServiceNameMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _serviceName;

    /// <summary>
    /// Инициализирует новый экземпляр класса ServiceNameMiddleware/>.
    /// </summary>
    /// <param name="next">Следующий делегат запроса в конвейере..</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public ServiceNameMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next ?? (context => Task.CompletedTask);
        _serviceName = configuration
            .GetSection("ServiceName")
            .Get<string>() ?? string.Empty;
    }

    /// <summary>
    /// Вызывает middleware для обработки запроса.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса и ответа.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Add("X-SERVICE-NAME",
            string.IsNullOrWhiteSpace(_serviceName) ? "Nameless loans application service" : _serviceName);
        await _next(context);
    }
}