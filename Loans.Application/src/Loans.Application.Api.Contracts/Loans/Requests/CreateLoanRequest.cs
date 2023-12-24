using Loans.Application.Api.Contracts.Dto;

namespace Loans.Application.Api.Contracts.Loans.Requests;

/// <summary>
/// Запрос создания кредитного контракта.
/// </summary>
/// <param name="ClientId">Id клиента.</param>
/// <param name="Amount">Желаемая сумма.</param>
/// <param name="LoanTermInYears">Срок кредита в годах.</param>
public record CreateLoanRequest(long ClientId, decimal Amount, int LoanTermInYears);