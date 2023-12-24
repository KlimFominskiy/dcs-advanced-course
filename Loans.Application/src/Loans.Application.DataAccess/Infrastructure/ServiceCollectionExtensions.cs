using System.Collections.Concurrent;
using Loans.Application.AppServices;
using Loans.Application.AppServices.Clients.Repositories;
using Loans.Application.AppServices.Contracts;
using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Loans.Repositories;
using Loans.Application.AppServices.Mappers;
using Loans.Application.DataAccess.Clients.Storages.InMemoryStorage;
using Loans.Application.DataAccess.Clients.Storages.InMemoryStorage.Repositories;
using Loans.Application.DataAccess.Loans.Storages.InMemoryStorage;
using Loans.Application.DataAccess.Loans.Storages.InMemoryStorage.Repositories;
using Loans.Application.DataAccess.Models;
using Loans.Application.DataAccess.Storages.InMemoryStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Application.DataAccess.Infrastructure;

/// <summary>
/// Расширения для настройки доступа к данным в приложении Loans.Application.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Мапперы модели в сущность.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddBusinessModelToEntityMappers(this IServiceCollection services)
    {
        services.AddSingleton<IMapper<Client, ClientEntity>, Mapper<Client, ClientEntity>>();
        services.AddSingleton<IMapper<Loan, LoanEntity>, Mapper<Loan, LoanEntity>>();
    }
    
    /// <summary>
    /// Мапперы сущности в модель.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddEntityToBusinessModelMappers(this IServiceCollection services)
    {
        services.AddSingleton<IMapper<ClientEntity, Client>, Mapper<ClientEntity, Client>>();
        services.AddSingleton<IMapper<LoanEntity, Loan>, Mapper<LoanEntity, Loan>>();
    }
    
    /// <summary>
    /// Добавляет компоненты доступа к данным.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddDataAccess(this IServiceCollection services)
    { 
        services.AddSingleton<ConcurrentDictionary<long, ClientEntity>>();
        services.AddSingleton<ConcurrentDictionary<long, LoanEntity>>();
        
        services.AddSingleton<IInMemoryStorage<Client, ClientEntity>, ClientsInMemoryStorage>();
        services.AddSingleton<IInMemoryStorage<Loan, LoanEntity>, LoansInMemoryStorage>();
        
        services.AddScoped<IClientsRepository, ClientsRepository>();
        services.AddScoped<ILoansRepository, LoansRepository>();
    }
}