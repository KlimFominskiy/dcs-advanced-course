using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Validators;

namespace Loans.Application.AppServices.Contracts.Loans.Validators;

/// <summary>
/// Валидатор модели данных кредитного договора.
/// </summary>
public interface ILoanValidator : IValidator<LoanDto>
{
    
}