namespace Loans.Application.AppServices.Contracts.Models;

/// <summary>
/// Ограничения по параметрам заявки на кредит.
/// </summary>
/// <param name="MinLoanAmount">Минимальная допустимая сумма кредита.</param>
/// <param name="MaxLoanAmount">Максимальная допустимая сумма кредита.</param>
/// <param name="MinLoanTermInYears">Минимальный допустимый срок кредита в годах.</param>
/// <param name="MaxLoanTermInYears">Максимальный допустимый срок кредита в годах.</param>
/// <param name="MinMonthlyIncome">Минимальная допустимая заработная плата клиента.</param>
public record LoanSpecification(decimal MinLoanAmount, decimal MaxLoanAmount, int MinLoanTermInYears,
    int MaxLoanTermInYears, decimal MinMonthlyIncome);