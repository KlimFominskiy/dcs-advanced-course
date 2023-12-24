using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.Api.Contracts.Clients.Responses;

namespace Loans.Application.Api.Contracts.Clients.Controllers;

/// <summary>
/// Контроллер для работы с моделью клиента.
/// </summary>
public interface IClientsController
{
    /// <summary>
    /// Метод получения коллекции кредитных контрактов по идентификатору клиента.
    /// </summary>
    /// <param name="clientId">Id клиента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список id договоров клиента.</returns>
    public Task<GetLoansByClientIdResponse> GetLoansByClientId(long clientId,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод поиска клиентов через фильтр.
    /// </summary>
    /// <param name="getClientByFilterRequest">Фильтр поиска клиента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список клиентов, удовлетворяющих фильтру.</returns>
    public Task<GetClientsByFilterResponse> GetClientsByFilter(GetClientByFilterRequest getClientByFilterRequest,
        CancellationToken cancellationToken);

    /// <summary>
    /// Метод создания клиента.
    /// </summary>
    /// <param name="createClientRequest">Данные создаваемого клиента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Id созданного клиента или причины отказа валидации.</returns>
    public Task<CreateClientResponse> CreateClient(CreateClientRequest createClientRequest,
        CancellationToken cancellationToken);
}