using Loans.Application.Api.Contracts.Dto;
using Loans.Application.Api.Contracts.Enums;

namespace Loans.Application.AppServices.Contracts.Loans.Handlers;

/// <summary>
/// Интерфейс обработки запросов по кредиту.
/// </summary>
public interface ILoanRequestHandlers
{
    /// <summary>
    /// Метод создания заявки на кредит.
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="amount"></param>
    /// <param name="loanTermInMonths"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Id созданного кредита.</returns>
    public Task<long> CreateLoan(long clientId, decimal amount, int loanTermInMonths, 
        CancellationToken cancellationToken);

    /// <summary>
    /// Метод получения статуса кредита по его id. 
    /// </summary>
    /// <param name="loanId">Id кредитного договора</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус кредита.</returns>
    public Task<LoanStatus> CheckLoanStatus(long loanId, CancellationToken cancellationToken);

    /// <summary>
    /// Метод поиска кредита по его id. 
    /// </summary>
    /// <param name="loanId">Id кредитного договора</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Данные о кредите.</returns>
    public Task<LoanDto> GetLoanById(long loanId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод по получению коллекции кредитных контрактов по идентификатору клиента.
    /// </summary>
    /// <param name="clientId">Id клиента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список кредитных договоров клиента.</returns>
    public Task<IReadOnlyCollection<LoanDto>> GetLoansByClientId(long clientId, CancellationToken cancellationToken);
}