using System.Net;
using Loans.Application.AppServices.Contracts.Clients.Exceptions;
using Loans.Application.AppServices.Infrastructure;
using Loans.Application.DataAccess.Infrastructure;
using Loans.Application.Host.Infrastructure;
using Loans.Application.Host.Middlewares;
using Microsoft.AspNetCore.Diagnostics;

namespace Loans.Application.Host;

/// <summary>
/// Конфигурация приложения.
/// </summary>
public class Startup
{
    /// <summary>
    /// Configuration of web application.
    /// </summary>
    private IConfiguration Configuration { get; }
    
    /// <summary>
    /// Application web host environment.
    /// </summary>
    private IWebHostEnvironment WebHostEnvironment { get; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="configuration">IConfiguration instance.</param>
    /// <param name="webHostEnvironment">WebHostEnvironment instance.</param>
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        WebHostEnvironment = webHostEnvironment;
    }
    
    /// <summary>
    /// Метод, в котором происходит конфигурация сервисов приложения. Таких как логирование, 
    /// доступ к БД, и к другим инфраструктурным вещам.
    /// </summary>
    /// <param name="services">Web app services collection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCustomServices();
        services.AddEntityToBusinessModelMappers();
        services.AddBusinessModelToEntityMappers();
        services.AddDtoToBusinessModelMappers();
        services.AddBusinessModelToDtoMappers();
        services.AddSwagger();
        services.AddValidators();
        services.AddRequestHandlers();
        services.AddSpecifications(Configuration);
        services.AddDecisionMakerService(Configuration);
        services.AddDataAccess();
    }
    
    /// <summary>
    /// Этот метод вызывается во время работы приложения (в рантайме).
    /// Здесь происходит конфигурация пайплайна обработки входящих запросов к серверу.
    /// </summary>
    /// <param name="app">IApplication Builder object.</param>
    /// <param name="env">IWwbHostEnvironment object.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionsMiddleware>();
        
        app.UseRouting();
        
        app.UseMiddleware<ServiceNameMiddleware>(Configuration);
        
        app.UseSwagger();
        app.UseSwaggerUI(s =>
        {
            s.SwaggerEndpoint("/swagger/Loans.Application v1.0/swagger.json", "Loans.Application API V1");
        });
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/healthz");
        });       
    }
}