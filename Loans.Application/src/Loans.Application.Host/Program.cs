namespace Loans.Application.Host;

/// <summary>
/// Главный класс приложения.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Точка входа приложения.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    private static Task Main(string[] args)
    {
        return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(app =>
            {
                app.UseStartup<Startup>();
            })
            .Build()
            .RunAsync();
    }
}