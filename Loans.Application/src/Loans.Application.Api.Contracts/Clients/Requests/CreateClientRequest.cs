namespace Loans.Application.Api.Contracts.Clients.Requests;

/// <summary>
/// Запрос создания клиента.
/// </summary>
/// <param name="FirstName">Имя клиента.</param>
/// <param name="MiddleName">Отчество клиента.</param>
/// <param name="LastName">Фамилия клиента.</param>
/// <param name="BirthDate">Дата рождения клиента.</param>
/// <param name="Salary">Заработная плата.</param>
public record CreateClientRequest(string FirstName, string MiddleName, string LastName, DateTime BirthDate, decimal Salary);