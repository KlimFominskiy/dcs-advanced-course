using Loans.Application.Api.Contracts.Dto;
using Loans.Application.Api.Contracts.Enums;
using Loans.Application.Api.Contracts.Loans.Requests;
using Loans.Application.Api.Contracts.Loans.Responses;

namespace Loans.Application.Api.Contracts.Loans.Controllers;

/// <summary>
/// Контроллер для работы с моделью кредита.
/// </summary>
public interface ILoansController
{
    /// <summary>
    /// Метод создания кредита.
    /// </summary>
    /// <param name="createLoanRequest">Данные создаваемого кредитного договора.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<CreateLoanResponse> CreateLoan(CreateLoanRequest createLoanRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Метод проверки статуса кредита.
    /// </summary>
    /// <param name="loanId">Id договора.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<LoanStatus> CheckLoanStatus(long loanId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод поиска кредита по его Id.
    /// </summary>
    /// <param name="loanId">Id договора.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<GetLoanByIdResponse> GetLoanById(long loanId, CancellationToken cancellationToken);
}