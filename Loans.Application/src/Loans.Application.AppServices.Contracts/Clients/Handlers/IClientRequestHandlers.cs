using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Models;

namespace Loans.Application.AppServices.Contracts.Clients.Handlers;

/// <summary>
/// Интерфейс обработки запросов по клиенту.
/// </summary>
public interface IClientRequestHandlers
{
    /// <summary>
    /// Поиск клиента по его id.
    /// </summary>
    /// <param name="clientId">Id клиента.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns>Данные о клиенте.</returns>
    public Task<ClientDto> GetClientById(long clientId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Поиск клиентов через фильтр.
    /// </summary>
    /// <param name="clientFilter">Параметры фильтрации.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns>Список клиентов, удовлетворяющих фильтру.</returns>
    public Task<IReadOnlyCollection<ClientDto>> GetClientsByFilter(ClientFilter clientFilter, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод создания клиента.
    /// </summary>
    /// <param name="firstName">Имя.</param>
    /// <param name="middleName">Отчество.</param>
    /// <param name="lastName">Фамилия.</param>
    /// <param name="birthDate">Дата рождения.</param>
    /// <param name="salary">Заработная плата.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Id созданного клиента.</returns>
    public Task<long> CreateClient(string firstName, string? middleName, string lastName, DateTime birthDate,
        decimal salary, CancellationToken cancellationToken);
}