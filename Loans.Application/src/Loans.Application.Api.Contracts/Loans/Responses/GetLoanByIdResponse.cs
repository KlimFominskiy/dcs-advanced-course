using Loans.Application.Api.Contracts.Dto;
using Loans.Application.Api.Contracts.Enums;

namespace Loans.Application.Api.Contracts.Loans.Responses; 

/// <summary>
/// Модель ответа на запрос поиска кредита по id.
/// </summary>
/// <param name="LoanDto">Данные о кредитном договоре.</param>
public record GetLoanByIdResponse(LoanDto LoanDto);