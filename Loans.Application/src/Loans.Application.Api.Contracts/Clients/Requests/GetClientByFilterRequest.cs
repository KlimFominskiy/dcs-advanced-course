namespace Loans.Application.Api.Contracts.Clients.Requests;

/// <summary>
/// Запрос поиска клиента по фильтру.
/// </summary>
/// <param name="FirstName">Имя клиента.</param>
/// <param name="MiddleName">Отчество клиента.</param>
/// <param name="LastName">Фамилия клиента.</param>
/// <param name="BirthDate">Дата рождения клиента.</param>
public record GetClientByFilterRequest(string? FirstName, string? MiddleName, string? LastName, DateTime? BirthDate);