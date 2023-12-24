using Loans.Application.Api.Contracts.Enums;

namespace Loans.Application.Api.Contracts.Loans.Responses;

/// <summary>
/// Ответ на запрос создания клиента.
/// </summary>
/// <param name="LoanId">Id клиента.</param>
/// <param name="LoanStatus">Статус кредитного договора.</param>
public record CreateLoanResponse(long LoanId, LoanStatus LoanStatus);