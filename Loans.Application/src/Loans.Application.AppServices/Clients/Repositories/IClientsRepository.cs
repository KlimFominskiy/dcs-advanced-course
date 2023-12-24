using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Models;

namespace Loans.Application.AppServices.Clients.Repositories;

/// <summary>
/// Репозиторий для получения из хранилищах данных по клиентам.
/// </summary>
public interface IClientsRepository
{
    /// <summary>
    /// Получить записи о клиентах, удовлетворяющих фильтру.
    /// </summary>
    /// <param name="clientFilter">Параметры для фильтрации.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns>Список клиентов, удовлетворяющих фильтру.</returns>
    public Task<IReadOnlyCollection<Client>> GetClientsByFilter(ClientFilter clientFilter, CancellationToken cancellationToken);

    /// <summary>
    /// Получить запись о клиенте по его Id.
    /// </summary>
    /// <param name="clientId">Id клиента.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns>Запись о клиенте.</returns>
    public Task<Client> GetClientById(long clientId, CancellationToken cancellationToken);

    /// <summary>
    /// Создать запись о клиенте.
    /// </summary>
    /// <param name="client">Новые данные о клиенте.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns>Id созданного клиента.</returns>
    public Task<long> CreateClient(Client client, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить запись о клиенте.
    /// </summary>
    /// <param name="clientId">Id записи о клиенте.</param>
    /// <param name="client">Новые данные о клиенте.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns>Обновлённая запись о клиенте.</returns>
    public Task<Client> UpdateClient(long clientId, Client client, CancellationToken cancellationToken);
}