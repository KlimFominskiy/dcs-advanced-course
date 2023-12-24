using System.Reflection;
using Loans.Application.AppServices.Contracts.HttpClients.DecisionMakerService;
using Loans.Application.Host.HttpClients.DecisionMakerService;
using Microsoft.OpenApi.Models;

namespace Loans.Application.Host.Infrastructure;

/// <summary>
/// Расширения для настройки сервисов в приложении Loans.Application.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет сервис принятия решений.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void AddDecisionMakerService(this IServiceCollection services, IConfiguration configuration)
    {
        DecisionMakerOptions decisionMakerOptions = configuration
            .GetSection("DecisionMakerOptions")
            .Get<DecisionMakerOptions>() ?? throw new InvalidOperationException("Не найдены настройки сервиса принятия решений.");
        services.AddSingleton(decisionMakerOptions);
        services.AddHttpClient<IDecisionMakerService, DecisionMakerService>();
    }
    
    /// <summary>
    /// Добавляет Swagger документацию.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("Loans.Application v1.0", new OpenApiInfo
            {
                Title = "Loans.Application",
                Version = "1.0",
            });
            
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
    }
}