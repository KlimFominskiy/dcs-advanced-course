using Loans.Application.Api.Contracts.Dto;

namespace Loans.Application.Api.Contracts.Clients.Responses;

/// <summary>
/// Ответ на запрос поиска клиента по фильтру.
/// <param name="ClientDto">Коллекция данных о клиентах.</param>
/// </summary>
public record GetClientsByFilterResponse(ClientDto[] ClientDto);