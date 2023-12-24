using Loans.Application.Api.Contracts.Dto;

namespace Loans.Application.Api.Contracts.Clients.Responses;

/// <summary>
/// Ответ на запрос поиска кредитов клиента по его Id.
/// </summary>
/// <param name="LoanDtos">Список кредитов клиента.</param>
public record GetLoansByClientIdResponse(LoanDto[] LoanDtos);