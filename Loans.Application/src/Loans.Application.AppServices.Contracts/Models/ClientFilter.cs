namespace Loans.Application.AppServices.Contracts.Models;

/// <summary>
/// Входная модель для фильтрации клиентов для обработчика клиентов.
/// </summary>
/// <param name="FirstName">Имя клиента.</param>
/// <param name="MiddleName">Отчество клиента.</param>
/// <param name="LastName">Фамилия клиента.</param>
/// <param name="BirthDate">Дата рождения клиента.</param>
public record ClientFilter(string? FirstName, string? MiddleName, string? LastName, DateTime? BirthDate);