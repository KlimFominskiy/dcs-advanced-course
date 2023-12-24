namespace Loans.Application.Api.Contracts.Clients.Responses;

/// <summary>
/// Ответ на запрос создания клиента.
/// </summary>
/// <param name="ClientId">Id клиента.</param>
public record CreateClientResponse(long ClientId);