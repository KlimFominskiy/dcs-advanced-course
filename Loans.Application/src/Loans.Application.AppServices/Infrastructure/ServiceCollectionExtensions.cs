using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Clients.Handlers;
using Loans.Application.AppServices.Clients.Validators;
using Loans.Application.AppServices.Contracts;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.AppServices.Contracts.Clients.Validators;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Validators;
using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Loans.Handlers;
using Loans.Application.AppServices.Loans.Validators;
using Loans.Application.AppServices.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Application.AppServices.Infrastructure;

/// <summary>
/// Расширения для настройки сервисов в приложении Loans.Application.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет кастомные сервисы.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddTransient<HttpClient>();
    }

    /// <summary>
    /// Добавляет валидаторы.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<ILoanValidator, LoanValidator>();
        services.AddScoped<IClientValidator, ClientValidator>();
    }
    
    
    /// <summary>
    /// Добавляет обработчики запросов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddRequestHandlers(this IServiceCollection services)
    {
        services.AddScoped<IClientRequestHandlers, ClientRequestHandlers>();
        services.AddScoped<ILoanRequestHandlers, LoanRequestHandlers>();
    }

    /// <summary>
    /// Добавляет спецификации из конфигурации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void AddSpecifications(this IServiceCollection services, IConfiguration configuration)
    {
        LoanSpecification loanSpecificationConfig = configuration
            .GetSection("LoanSpecification")
            .Get<LoanSpecification>() ?? throw new InvalidOperationException("Не найдена спецификация кредита.");
        services.AddSingleton(loanSpecificationConfig);
        
        ClientSpecification clientSpecification = configuration
            .GetSection("ClientSpecification")
            .Get<ClientSpecification>() ?? throw new InvalidOperationException("Не найдена спецификация клиента.");
        services.AddSingleton(clientSpecification);
    }
    
    /// <summary>
    /// Мапперы объекта, передающего данные, в модель.
    /// </summary>
    /// <param name="services"></param>
    public static void AddDtoToBusinessModelMappers(this IServiceCollection services)
    {
        services.AddSingleton<IMapper<ClientDto, Client>, Mapper<ClientDto, Client>>();
        services.AddSingleton<IMapper<LoanDto, Loan>, Mapper<LoanDto, Loan>>();
    }
    
    /// <summary>
    /// Мапперы модели в объект, передающий данные.
    /// </summary>
    /// <param name="services"></param>
    public static void AddBusinessModelToDtoMappers(this IServiceCollection services)
    {
        services.AddSingleton<IMapper<Client, ClientDto>, Mapper<Client, ClientDto>>();
        services.AddSingleton<IMapper<Loan, LoanDto>, Mapper<Loan, LoanDto>>();
    }
}