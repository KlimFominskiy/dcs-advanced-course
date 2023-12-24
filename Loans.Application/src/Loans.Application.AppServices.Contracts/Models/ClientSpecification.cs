namespace Loans.Application.AppServices.Contracts.Models;

/// <summary>
/// Ограничения по данным клиента.
/// </summary>
/// <param name="MaxAgeInYears">Максимальный возраст (в годах) для одобрения кредита.</param>
/// <param name="MinAgeInYears">Минимальный возраст (в годах) для одобрения кредита.</param>
public record ClientSpecification(int MaxAgeInYears, int MinAgeInYears);